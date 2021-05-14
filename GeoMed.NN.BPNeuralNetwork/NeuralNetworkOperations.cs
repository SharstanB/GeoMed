using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using GeoMed.NN.Base;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork.Basic;
using GeoMed.NN.BPNeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoMed.NN.BPNeuralNetwork
{
    internal static class NeuralNetworkOperations 
    {
        private static List<NNInput> TestDataList { get; set; }

        private static NNResult NNResult { get; set; } = new NNResult();
        public static (NeuralNetwork initializedNN , List<NNInput> samples) InitialLayers(NeuralNetwork neuralNetwork  , ExecutedData executedData)
         {

            var inputList = COVID19USCountry.GetStateInput(executedData);

            var trainDataList = inputList.trainData;

            TestDataList = inputList.testData;

            neuralNetwork.InputLayer.Input = trainDataList[0];

            int hiddenCells = (new Random().Next(2, 5));

            var firsthiddenLayer = new HiddenLayer(LayerType.Hidden, hiddenCells);
            firsthiddenLayer.Weights = InitialWeigths(hiddenCells , neuralNetwork.InputLayer.CellsCount);

            neuralNetwork.HiddenLayers.Add(firsthiddenLayer);

            for (int i = 1; i < neuralNetwork.HiddenLayerCount ; i++)
            {
                var newHiddenCells = new Random().Next(2,5);
                var hiddenLayer = new HiddenLayer(LayerType.Hidden, newHiddenCells);
                hiddenLayer.Weights = InitialWeigths(newHiddenCells, hiddenCells);
                neuralNetwork.HiddenLayers.Add(hiddenLayer);
                hiddenCells = newHiddenCells;
            }

            neuralNetwork.OutputLayer = new ElmanLayer(LayerType.Output, 1);

            neuralNetwork.OutputLayer.Weights =
               InitialWeigths( neuralNetwork.OutputLayer.CellsCount 
               , neuralNetwork.HiddenLayers.LastOrDefault().CellsCount);


            return (neuralNetwork , trainDataList.Skip(0).ToList());
        }


        public static List<List<double>> InitialWeigths(int inputCount  
                                                      , int outputCount 
                                                      , bool isContext = false)
        {
            List<double> ll = new List<double>();
            var list = new List<List<double>>(inputCount);
            for (int i = 0; i < inputCount; i++)
            {
                 ll = Enumerable.Range(0, outputCount).Select(s => isContext ? 1.1
                  : new Random().NextDouble()).ToList();
                 list.Add(ll);
            }
            return list;
        }
       
        private static NeuralNetwork ForwordOperation(NeuralNetwork elmanBPNeural)
        {
            elmanBPNeural.HiddenLayers.FirstOrDefault().LayerCells.Clear();

            var layer = elmanBPNeural.HiddenLayers.FirstOrDefault().HiddenContextLayer.FirstOrDefault();

            elmanBPNeural.HiddenLayers.FirstOrDefault().Weights.ForEach(weigth =>
            {

               var inputRes = 
                         (weigth[0] * elmanBPNeural.InputLayer.Input.Cases +
                          weigth[1] * elmanBPNeural.InputLayer.Input.MedianAge +
                          weigth[2] * elmanBPNeural.InputLayer.Input.Population);

                elmanBPNeural.HiddenLayers.FirstOrDefault()
              .LayerCells.Add(new Neuron()
              { value = ActivationFunctions.Tanh(elmanBPNeural.HiddenLayers.FirstOrDefault().GetContextValues
                      (inputRes, elmanBPNeural.NNType))});
              });
           

            if (elmanBPNeural.NNType == NNType.Elman)
             elmanBPNeural.HiddenLayers.FirstOrDefault().FillHiddenCellValues();

            for (int i = 1; i < elmanBPNeural.HiddenLayerCount; i++) // loop for hidden layers
                // , first hidden layer calculated in last step from input values
            {
                elmanBPNeural.HiddenLayers[i].LayerCells.Clear();
                for (int j = 0; j < elmanBPNeural.HiddenLayers[i].CellsCount; j++) // loop for set each hidden layer cell
                {
                    var res = 0.0;
                    for (int k = 0; k < elmanBPNeural.HiddenLayers[i - 1].CellsCount; k++)
                    {
                        var cell = elmanBPNeural.HiddenLayers[i - 1].LayerCells[k];
                        var weigth = elmanBPNeural.HiddenLayers[i]
                              .Weights[j][k];
                        res += cell.value * weigth;
                    }
                    
                    elmanBPNeural.HiddenLayers[i].LayerCells.Add(new Neuron()
                    { value = ActivationFunctions.Tanh(elmanBPNeural.HiddenLayers[i].GetContextValues(res , elmanBPNeural.NNType)) });
                }

                if (elmanBPNeural.NNType == NNType.Elman)
                    elmanBPNeural.HiddenLayers[i].FillHiddenCellValues();
            }
            elmanBPNeural.OutputLayer.LayerCells.Clear();
            elmanBPNeural.OutputLayer.Weights.ForEach(weigth =>
                {
                    var res = 0.0;
                    weigth.ForEach(f =>
                    {
                        elmanBPNeural.HiddenLayers.LastOrDefault().LayerCells.ForEach(cellVal =>
                        {
                            var idx = elmanBPNeural.HiddenLayers.LastOrDefault().LayerCells.IndexOf(cellVal);
                            res += cellVal.value * f  + elmanBPNeural.HiddenLayers.LastOrDefault()
                            .HiddenContextLayer
                           .FirstOrDefault().LayerCells[idx].value;
                        });
                    });
                    elmanBPNeural.OutputLayer.LayerCells.Add(new Neuron() { value = ActivationFunctions.Linear(res) });
                });

            return elmanBPNeural;
        }

        public static NeuralNetwork TrainNN((NeuralNetwork elmanBPNeural, List<NNInput> samples) model)
        {
            
            while (model.elmanBPNeural.Epochs-- > 0 )
            {
                var list = new List<SampleResult>();
                var finalEpochError = 0.0;
                foreach (var sample in model.samples)
                {
                    model.elmanBPNeural.InputLayer.Input = sample; 
                    var forwordResult = ForwordOperation(model.elmanBPNeural);

                    list.Add(new SampleResult()
                    {
                        ActualOutput = forwordResult.OutputLayer.
                              LayerCells.FirstOrDefault().value ,
                        TargetOutput = sample.TargetOutput
                    });    
                   var Error = forwordResult.InputLayer.Input.NNPBError
                        (forwordResult.OutputLayer.LayerCells.Select(s => s.value).ToList());

                    if (Error.ErrorObtained > model.elmanBPNeural.ErrorRate)
                    {
                        model.elmanBPNeural = CalcNeuronsErrors(model.elmanBPNeural, Error.NetworkError);

                        UpdateWeigths(model.elmanBPNeural);
                    }
                    finalEpochError += Error.ErrorObtained;


                }
                NNResult.TrainSamples.Add((samples: list, epoch: ( model.elmanBPNeural.Epochs + 1) 
                    , finalEpochError / model.samples.Count));
            }
            return model.elmanBPNeural;
        }

        private static NeuralNetwork CalcNeuronsErrors (NeuralNetwork elmanBPNeural , double networkError)
        {
            elmanBPNeural.OutputLayer.LayerCells.ForEach(output =>
            {
                output.Error = (ErrorFunctions.ClacErrorHelper(output.value) * networkError);
            });
            var LastHiddenLayer = elmanBPNeural.HiddenLayers.LastOrDefault();
            for (int i = 0; i < LastHiddenLayer.CellsCount; i++)
            {
                var res = 0.0;
                for (int k = 0; k < elmanBPNeural.OutputLayer.CellsCount; k++)
                {
                    res += elmanBPNeural.OutputLayer.Weights[k][i] *
                        elmanBPNeural.OutputLayer.LayerCells[k].Error.Value;
                }
                LastHiddenLayer.LayerCells[i].Error = (res * ErrorFunctions.ClacErrorHelper(LastHiddenLayer.LayerCells[i].value));
            }


            for (int j = elmanBPNeural.HiddenLayerCount - 2; j >= 0; j--)
            {
                for (int i = 0; i < elmanBPNeural.HiddenLayers[j].CellsCount; i++)
                {
                    var res = 0.0;
                    for (int k = 0; k < elmanBPNeural.HiddenLayers[j + 1].CellsCount; k++)
                    {
                        res += elmanBPNeural.HiddenLayers[j + 1].Weights[k][i] *
                            elmanBPNeural.HiddenLayers[j + 1].LayerCells[k].Error.Value;
                    }
                    elmanBPNeural.HiddenLayers[j].LayerCells[i].Error = res *
                       ErrorFunctions.ClacErrorHelper(elmanBPNeural.HiddenLayers[j].LayerCells[i].value);
                }
            }
            return elmanBPNeural;
        }
       
        public static NeuralNetwork UpdateWeigths(NeuralNetwork elmanBPNeural)
        {
            for (int i = 0; i < elmanBPNeural.HiddenLayers.LastOrDefault().CellsCount; i++)
            {
                for (int j = 0; j < elmanBPNeural.OutputLayer.CellsCount; j++)
                {
                    elmanBPNeural.OutputLayer.Weights[j][i] +=
                         elmanBPNeural.Alpha * elmanBPNeural.OutputLayer.LayerCells[j].Error.Value *
                          elmanBPNeural.HiddenLayers.LastOrDefault().LayerCells[i].value;
                }
            }
            for (int i = elmanBPNeural.HiddenLayerCount - 1; i > 0 ; i--)
            {
                for (int j = 0; j < elmanBPNeural.HiddenLayers[i].CellsCount; j++)
                {
                    for (int k = 0; k < elmanBPNeural.HiddenLayers[i - 1].CellsCount; k++)
                    {
                        elmanBPNeural.HiddenLayers[i].Weights[j][k] +=
                          elmanBPNeural.Alpha * elmanBPNeural.HiddenLayers[i - 1].LayerCells[k].value *
                           elmanBPNeural.HiddenLayers[i].LayerCells[j].Error.Value;
                    }
                   
                }
            }
                for (int j = 0; j < elmanBPNeural.HiddenLayers.FirstOrDefault().CellsCount; j++)
                {
                    elmanBPNeural.HiddenLayers.FirstOrDefault().Weights[j][0] +=
                         elmanBPNeural.Alpha * elmanBPNeural.HiddenLayers.FirstOrDefault().LayerCells[j].Error.Value *
                         elmanBPNeural.InputLayer.Input.Cases ;

                elmanBPNeural.HiddenLayers.FirstOrDefault().Weights[j][1] +=
                         elmanBPNeural.Alpha * elmanBPNeural.HiddenLayers.FirstOrDefault().LayerCells[j].Error.Value *
                         elmanBPNeural.InputLayer.Input.MedianAge;

                elmanBPNeural.HiddenLayers.FirstOrDefault().Weights[j][2] +=
                         elmanBPNeural.Alpha * elmanBPNeural.HiddenLayers.FirstOrDefault().LayerCells[j].Error.Value *
                         elmanBPNeural.InputLayer.Input.Population;
            }
            return elmanBPNeural;
        }

        public static NNResult GetResult( NeuralNetwork elmanBPNeural   )
        {
            elmanBPNeural.HiddenLayers.ForEach(hLayer =>
            {
                NNResult.FinalWeigths.Add(new FinalWeigth()
                {
                    LayerType = LayerType.Hidden,
                    HiddenNumber = elmanBPNeural.HiddenLayers.IndexOf(hLayer) + 1,
                    weigths = hLayer.Weights
                });
            });
            NNResult.FinalWeigths.Add(new FinalWeigth()
            {
                LayerType = LayerType.Output,
                weigths =  elmanBPNeural.OutputLayer.Weights
            });

            return NNResult;
        }

        public static NeuralNetwork TestNN(NeuralNetwork elmanBPNeural)
        {
            foreach (var sample in TestDataList)
            {
                elmanBPNeural.InputLayer.Input = sample;

                var forwordResult = ForwordOperation(elmanBPNeural);

                NNResult.TestSamples.Add(new SampleResult()
                {
                    TargetOutput = sample.TargetOutput,
                    ActualOutput = forwordResult.OutputLayer.
                    LayerCells.FirstOrDefault().value
                });
            }

            NNResult.NetworkError = NNResult.TestSamples.Select(s=> (s.TargetOutput , s.ActualOutput))
                .MeanSquaredError();
            return elmanBPNeural;
        }

        private static ElmanLayer FillHiddenCellValues(this HiddenLayer elmanLayer )
        {
            for (int i = elmanLayer.HiddenContextLayersCount - 2 ; i >= 0 ; i--)
            {
                elmanLayer.HiddenContextLayer[i + 1].LayerCells = 
                elmanLayer.HiddenContextLayer[i].LayerCells.GetClone().ToList();
            }

            elmanLayer.HiddenContextLayer.FirstOrDefault().LayerCells = 
                elmanLayer.LayerCells.GetClone();

            return elmanLayer;
        }


        private static double GetContextValues(this HiddenLayer source ,
            double cellVal ,  NNType nNType = NNType.BackProbagation)
        {
            var res = cellVal;
            if(nNType == NNType.Elman)
            source.HiddenContextLayer.ForEach(layer =>
            {
                layer.LayerCells.ForEach(cell =>
                {
                    res += cell.value;
                });
            });
            return res;
        }
    }
}

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
        /// <summary>
        ///  temporary storage for testing samples
        /// </summary>
        private static List<NNInput> TestDataList { get; set; }

        /// <summary>
        ///  represent final result 
        /// </summary>
        private static NNResult NNResult { get; set; } = new NNResult();

        /// <summary>
        ///  initial neural network or we can say built network with initial values 
        ///  for traing operation
        /// </summary>
        /// <param name="neuralNetwork">get network properties </param>
        /// <param name="executedData"> which data will be trained</param>
        /// <returns></returns>
        public static (NeuralNetwork initializedNN , List<NNInput> samples) InitialLayers(NeuralNetwork neuralNetwork  , ExecutedData executedData)
         {
            // get data for train and test 
            var inputList = COVID19USCountry.GetDataInput(executedData);

            var trainDataList = inputList.trainData;

            TestDataList = inputList.testData;

            neuralNetwork.InputLayer.Input = trainDataList[0];// initial with first sample

            int hiddenCells = (new Random().Next(2, 5)); // first hidden layer cells

            var firsthiddenLayer = new HiddenLayer(LayerType.Hidden, hiddenCells); // first layer

            firsthiddenLayer.Weights = InitialWeigths(hiddenCells , 
                neuralNetwork.InputLayer.CellsCount); // initial first layer weigths

            neuralNetwork.HiddenLayers.Add(firsthiddenLayer); // add first hidden layer to hidden layers list

            // complete with other hidden layer if layers count more than one 
            for (int i = 1; i < neuralNetwork.HiddenLayerCount ; i++)
            {
                var newHiddenCells = new Random().Next(2,5);
                var hiddenLayer = new HiddenLayer(LayerType.Hidden, newHiddenCells);
                hiddenLayer.Weights = InitialWeigths(newHiddenCells, hiddenCells);
                neuralNetwork.HiddenLayers.Add(hiddenLayer);
                hiddenCells = newHiddenCells;
            }

            // create output layer 
            neuralNetwork.OutputLayer = new ElmanLayer(LayerType.Output, 1);

            // initial output weigths 
            neuralNetwork.OutputLayer.Weights =
               InitialWeigths( neuralNetwork.OutputLayer.CellsCount 
               , neuralNetwork.HiddenLayers.LastOrDefault().CellsCount);

            // return initialed netword which is ready for training
            return (neuralNetwork , trainDataList.Skip(0).ToList());
        }


        /// <summary>
        ///  initial Network weigths between two layers (from , to) 
        /// </summary>
        /// <param name="inputCount">from layer cells count</param>
        /// <param name="outputCount">to layer cells count</param>
        /// <param name="isContext">if these weigths for context layers or not</param>
        /// <returns></returns>
        public static List<List<double>> InitialWeigths(int inputCount  
                                                      , int outputCount 
                                                      , bool isContext = false)
        {
            List<double> ll = new List<double>();
            var list = new List<List<double>>(inputCount);
            for (int i = 0; i < inputCount; i++)
            {
                 ll = Enumerable.Range(0, outputCount).Select(s => isContext ? 1.1
                 // ifcontext layer initial with 1 or with random value between  (1 , 0)
                 : new Random().NextDouble()).ToList(); 
                 list.Add(ll);
            }
            return list;
        }
        /// <summary>
        ///  move from input layer to output layer across hidden layers 
        /// </summary>
        /// <param name="elmanBPNeural"></param>
        /// <returns></returns>
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

             // calculate first hidden layer 
                elmanBPNeural.HiddenLayers.FirstOrDefault()
              .LayerCells.Add(new Neuron()
              { value = ActivationFunctions.Tanh(elmanBPNeural.HiddenLayers.FirstOrDefault().GetContextValues
                      (inputRes, elmanBPNeural.NNType))});
              });
           

            if (elmanBPNeural.NNType == NNType.Elman)
             elmanBPNeural.HiddenLayers.FirstOrDefault().FillContextCellValues();

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
                    elmanBPNeural.HiddenLayers[i].FillContextCellValues();
            }
            elmanBPNeural.OutputLayer.LayerCells.Clear();
            // calculate ouput layer 
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
        /// <summary>
        /// trai network with two step (feed forword - back forword)
        /// </summary>
        /// <param name="model">get network and data for training</param>
        /// <returns></returns>
        public static NeuralNetwork TrainNN((NeuralNetwork elmanBPNeural, List<NNInput> samples) model)
        {
            
            while (model.elmanBPNeural.Epochs-- > 0 )
            {
                var list = new List<SampleResult>();
                var finalEpochError = 0.0;
                foreach (var sample in model.samples) // loop on train samples 
                {
                    model.elmanBPNeural.InputLayer.Input = sample; 
                    var forwordResult = ForwordOperation(model.elmanBPNeural);

                    list.Add(new SampleResult() // store (actual - target) outputs for each sample
                    {
                        ActualOutput = forwordResult.OutputLayer.
                              LayerCells.FirstOrDefault().value ,
                        TargetOutput = sample.TargetOutput
                    });    

                    // calculate error for each sample
                   var Error = forwordResult.InputLayer.Input.NNPBError
                        (forwordResult.OutputLayer.LayerCells.Select(s => s.value).ToList());

                    // using calculate error to decide update weigths or not 
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
       /// <summary>
       /// update weigths (back forword)
       /// </summary>
       /// <param name="elmanBPNeural"></param>
       /// <returns></returns>
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
        /// <summary>
        ///  store final weigths for trained network
        /// </summary>
        /// <param name="neuralNetwork"></param>
        /// <returns></returns>
        public static NNResult GetResult( NeuralNetwork neuralNetwork   )
        {

            neuralNetwork.HiddenLayers.ForEach(hLayer =>
            {
                NNResult.FinalWeigths.Add(new FinalWeigth()
                {
                    LayerType = LayerType.Hidden,
                    HiddenNumber = neuralNetwork.HiddenLayers.IndexOf(hLayer) + 1,
                    weigths = hLayer.Weights
                });
            });
            NNResult.FinalWeigths.Add(new FinalWeigth()
            {
                LayerType = LayerType.Output,
                weigths = neuralNetwork.OutputLayer.Weights
            });

            return NNResult;
        }
        /// <summary>
        /// test trained network with test data samples (calc error without update weigths)
        /// </summary>
        /// <param name="elmanBPNeural"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elmanLayer"></param>
        /// <returns></returns>
        private static ElmanLayer FillContextCellValues(this HiddenLayer elmanLayer )
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

        public static void LoadDataToNetwork(NNInput nNInput)
        {
            
        }
    }
}

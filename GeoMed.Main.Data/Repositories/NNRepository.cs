using GeoMed.Main.IData.IRepositories;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using GeoMed.Base;
using GeoMed.Main.DTO.Settings;
using GeoMed.NN.BPNeuralNetwork;

namespace GeoMed.Main.Data.Repositories
{
    public class NNRepository : INNRepository
    {
        private string ModelPath = "wwwroot/models";

        public void SaveTrainedModel(NNResult nNResult)
        {
            if (!Directory.Exists(ModelPath))
            {
                Directory.CreateDirectory(ModelPath);
            }

         //   XElement xElement = new XElement(nameof(nNResult.NetworkError), nNResult.NetworkError);

            XElement xElement = new XElement("ModelInformation", new XElement( nameof(nNResult.NetworkError) 
                , nNResult.NetworkError));


            List<XElement> samples = new List<XElement>();

            int idx = 1;
            //nNResult.TrainSamples.ForEach(sample =>
            //{
            //    samples.Add(new XElement( $"Sample" , new XAttribute("Value" , idx++), 
            //        new XElement(nameof(sample.epoch), sample.epoch),
            //        new XElement(nameof(sample.error),  sample.error),
            //        new XElement(nameof(sample.samples), sample.samples)));
            //});

            //xElement.Add(new XElement(nameof(nNResult.TrainSamples), samples));


            //idx = 1;
            //samples.Clear();
            //nNResult.TestSamples.ForEach(sample =>
            //{
            //    samples.Add( new XElement($"Sample", new XAttribute("Value", idx++),
            //        new XElement(nameof(sample.ActualOutput),  sample.ActualOutput),
            //        new XElement(nameof(sample.NeuronError),  sample.NeuronError),
            //        new XElement(nameof(sample.TargetOutput), sample.TargetOutput)));
            //});

            //xElement.Add( new XElement( nameof(nNResult.TestSamples), samples));


            //samples.Clear();

            nNResult.FinalWeigths.ForEach(weigthItem =>
            {

                var element = new List<XElement>();

                idx = 1;
                weigthItem.weigths.ForEach(outWeigth =>
                {
                    outWeigth.ForEach(inWeigth => {
                        element.Add(new XElement("weigth",
                            new XAttribute("out", weigthItem.weigths.IndexOf(outWeigth))
                            , new XAttribute("in", outWeigth.IndexOf(inWeigth))
                           , inWeigth));
                    });
                });

                samples.Add(new XElement((weigthItem.LayerType == LayerType.Hidden  ? 
                                                          $"{nameof(LayerType.Hidden)}{weigthItem.HiddenNumber}" :
                                                           nameof(LayerType.Output)), element));
            });

            xElement.Add( new XElement(nameof(nNResult.FinalWeigths), samples));


            //var finalElem = new XElement("ModelInformation", xElement);

            xElement.Save(Path.Combine( ModelPath,$"{@"model-"}{DateTime.Now}.xml"));
        }

        public OperationResult<NNResult> TrainNeuralNetwork(TrainNeuralNetworkDto trainNeural)
        {
            OperationResult<NNResult> operationResult = new OperationResult<NNResult>();
            NNResult nNResult = NeuralNetworkAPI.
                GetElmanNetworkResult(trainNeural.Epochs, trainNeural.ErrorRate,
                trainNeural.ExecutedData, trainNeural.HiddenLayersCount);
            operationResult.Result = nNResult;
            operationResult.OperationResultType = OperationResultTypes.Success;

            SaveTrainedModel(nNResult);

            return operationResult;
        }


        public OperationResult<bool> LoadModel()
        {
            var operation = new OperationResult<bool>();


            return operation;
        }
    }
}

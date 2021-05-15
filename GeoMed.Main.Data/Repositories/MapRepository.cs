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

namespace GeoMed.Main.Data.Repositories
{
    public class MapRepository : IMapRepository
    {
        private string ModelPath = Path.Combine(Directory.GetCurrentDirectory(), "models");

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

            xElement.Save(Path.Combine( ModelPath,$"{@"model-"}{Guid.NewGuid()}.xml"));
        }
    }
}

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
using GeoMed.SqlServer;
using GeoMed.Model.DataSet;
using AdvanceNN;
using Microsoft.EntityFrameworkCore;

namespace GeoMed.Main.Data.Repositories
{
    public class NNRepository : BaseRepository ,  INNRepository
    {

        #region Properties
        private string ModelPath = "wwwroot/models";

        #endregion

        #region Constructer

        public NNRepository(GMContext context)
            : base(context)
        {

        }
        #endregion

        public OperationResult<bool> SaveTrainedModel(NNResult nNResult)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (!Directory.Exists(ModelPath))
                {
                    Directory.CreateDirectory(ModelPath);
                }
                XElement xElement = new XElement("ModelInformation", new XElement(nameof(nNResult.NetworkError)
                    , nNResult.NetworkError) , new XElement(nameof(NNType), nNResult.NNType));


                List<XElement> samples = new List<XElement>();
               
                nNResult.FinalWeigths.ForEach(weigthItem =>
                {

                    var element = new List<XElement>();

                    weigthItem.weigths.ForEach(outWeigth =>
                    {
                        var weigthElem = new List<XElement>();

                        outWeigth.ForEach(inWeigth => {

                            weigthElem.Add(new XElement("weigth",
                                new XAttribute("out", weigthItem.weigths.IndexOf(outWeigth))
                                , new XAttribute("in", outWeigth.IndexOf(inWeigth))
                               , inWeigth));
                        });

                        element.Add( new XElement($"group{(weigthItem.weigths.IndexOf(outWeigth) + 1)}", weigthElem));
                    });

                    samples.Add(new XElement((weigthItem.LayerType == LayerType.Hidden ?
                                                              $"{nameof(LayerType.Hidden)}{weigthItem.HiddenNumber}" :
                                                               nameof(LayerType.Output))
                                                               , new XAttribute("number", weigthItem.HiddenNumber), element));
                });

                xElement.Add(new XElement(nameof(nNResult.FinalWeigths), samples));

                var finalPath = Path.Combine(ModelPath, $"{@"model-"}{ DateTime.Now.ToString("yyyy-MM-dd") + DateTime.Now.TimeOfDay.ToString().Replace(":", "-") }.xml");

                xElement.Save(finalPath);

                Context.Models.Add(new ModelSet()
                {
                    ErrorRate = nNResult.NetworkError,
                    Path = finalPath,
                    AlgorithmType = (int)nNResult.NNType,
                    ExecutedDataType = (int)nNResult.ExecutedData,
                });

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.OperationResultType = OperationResultTypes.Exeption;
            }

            return result;
        }

        public OperationResult<NNResult> TrainNeuralNetwork(TrainNeuralNetworkDto trainNeural)
        {
            OperationResult<NNResult> operationResult = new OperationResult<NNResult>();
            NNResult nNResult = NeuralNetworkAPI.
                GetNetworkResult( trainNeural.Epochs, trainNeural.ErrorRate,
                trainNeural.ExecutedData, trainNeural.HiddenLayersCount , trainNeural.NNType);
            nNResult.NNType = trainNeural.NNType;
            nNResult.ExecutedData = trainNeural.ExecutedData;
            operationResult.Result = nNResult;
            operationResult.OperationResultType = OperationResultTypes.Success;

            SaveTrainedModel(nNResult);

            return operationResult;
        }


        public OperationResult<NNResult> LoadModel()
        {
            var operation = new OperationResult<NNResult>();

            NNResult nResult = new NNResult();

            string filePath = ChooseMoreAccurateModel();


            XDocument xdoc = XDocument.Load(filePath);

            var xElement = xdoc.Descendants("FinalWeigths");
             

            if (xElement != null)
                for (int i = 0; i < xElement.Elements().Count() ; i++)
                {
                    var child = xElement.Elements().ToList()[i];
                    var finalWeigth = new FinalWeigth();
                    if (child.Name.ToString().Contains(nameof(LayerType.Hidden)))
                    {
                        finalWeigth.LayerType = LayerType.Hidden;
                        finalWeigth.HiddenNumber = Convert.ToInt32(child.Attribute("number").Value);

                    }
                    else
                    {
                        finalWeigth.LayerType = LayerType.Output;
                    }

                    finalWeigth.weigths = new List<List<double>>();
                    foreach (var childElement in child.Elements())
                        {

                            var temp = new List<double>();
                            foreach (var childchild in childElement.Elements())
                            {
                                var outAttr = Convert.ToInt32(childchild.Attributes().FirstOrDefault().Value);
                                var inAttr = Convert.ToInt32(childchild.Attributes().LastOrDefault().Value);

                                temp.Insert(inAttr, Convert.ToDouble(childchild.Value));
                                try
                                {
                                    finalWeigth.weigths.RemoveAt(outAttr);
                                }
                                catch
                                {

                                }
                                finalWeigth.weigths.Insert(outAttr, temp);
                            }
                        }
                   

                    nResult.FinalWeigths.Add(finalWeigth);

                }

            NeuralNetworkAPI.loadModel(nResult);

            operation.Result = nResult;
            operation.OperationResultType = OperationResultTypes.Success;

            return operation;
        }


        public OperationResult<List<int>> LoadPredicateData()
        {
            var data = Context.SpatialInfos.Include(a=>a.CovidZones).
                ToList()
                .GroupBy(g => g.fib).SelectMany(item => item.Select(covid => 
                  covid.CovidZones.Where(a=>a.Cases >= 0).OrderBy(o=>o.Date).Select(cov => new { a = cov.Cases,
                      b = cov.FipsCode , c = cov.SpatialInfoId , d= cov.Id}
                 ).SkipLast(10).TakeLast(100))).Take(100).ToList();

            var result = new List<int>();
            var test = data.Select(a => a.Select(b => b)).ToList();
            data.ForEach(item =>
            {
                result.Add(AdvanceNetwork.Forecasting(item.Select(dd => new float[][] 
                { new float[] { ((float)dd.a) } }).ToList()));
            });

            return new OperationResult<List<int>>();
        } 

        private string ChooseMoreAccurateModel()
        => Context.Models.OrderBy(o => o.ErrorRate).FirstOrDefault().Path;
    }
}

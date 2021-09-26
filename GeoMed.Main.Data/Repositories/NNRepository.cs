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
using GeoMed.Main.DTO.Forcast;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.IO;

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


        public OperationResult<List<ForcastDto>> LoadPredicateData()
        {
            var data = Context.SpatialInfos
                .Include(a=>a.CovidZones).ToList()
                .GroupBy(g => g.fib).Select(item => new {
                 cases =  item.SelectMany(covid =>
                  covid.CovidZones.Where(a => a.Cases >= 0)
                  .OrderBy(o => o.Date)
                  .Select(cov => cov.Cases).SkipLast(10)),
                    fib = item.Key,
                    state = item.FirstOrDefault().State,
                    lat = item.FirstOrDefault().Lat,
                    lng = item.FirstOrDefault().Long}
                  ).ToList();

            var result = new OperationResult<List<ForcastDto>>();
            result.Result = new List<ForcastDto>();
            // var test = data.Select(a => a.Select(b => b)).ToList();
            foreach (var item in data)
            {
                var ff = AdvanceNetwork.Forecasting(item.cases
                .Select(dd => new float[][]
                { new float[] { (float)dd } }).ToList());
                result.Result.Add(
                    new ForcastDto
                    {
                        Cases = ff ,
                        Fib = item.fib,
                        Lang = item.lng,
                        Lat = item.lat,
                        StateCode = item.state
                    });
            }
            //data.ForEach(item =>
            //{
                
            //});
            Reader.Write(result.Result);

            return result;
        } 

        private string ChooseMoreAccurateModel()
        => Context.Models.OrderBy(o => o.ErrorRate).FirstOrDefault().Path;
    }
}

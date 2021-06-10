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
                    , nNResult.NetworkError));

                xElement.Add(new XElement("NNType")
                               , nNResult.NNType);

                List<XElement> samples = new List<XElement>();

                nNResult.FinalWeigths.ForEach(weigthItem =>
                {

                    var element = new List<XElement>();

                    weigthItem.weigths.ForEach(outWeigth =>
                    {
                        outWeigth.ForEach(inWeigth => {
                            element.Add(new XElement("weigth",
                                new XAttribute("out", weigthItem.weigths.IndexOf(outWeigth))
                                , new XAttribute("in", outWeigth.IndexOf(inWeigth))
                               , inWeigth));
                        });
                    });

                    samples.Add(new XElement((weigthItem.LayerType == LayerType.Hidden ?
                                                              $"{nameof(LayerType.Hidden)}{weigthItem.HiddenNumber}" :
                                                               nameof(LayerType.Output)), element));
                });

                xElement.Add(new XElement(nameof(nNResult.FinalWeigths), samples));

                var finalPath = Path.Combine(ModelPath, $"{@"model-"}{ DateTime.Now.ToString("yyyy-MM-dd") + DateTime.Now.TimeOfDay.ToString().Replace(":", "-") }.xml");

                xElement.Save(finalPath);

                Context.Models.Add(new ModelSet()
                {
                    ErrorRate = nNResult.NetworkError,
                    Path = finalPath,
                    AlgorithmType = (int)nNResult.NNType,
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
            operationResult.Result = nNResult;
            operationResult.OperationResultType = OperationResultTypes.Success;

            SaveTrainedModel(nNResult);

            return operationResult;
        }


        public OperationResult<bool> LoadModel(string filePath)
        {
            var operation = new OperationResult<bool>();

            NNResult nResult = new NNResult();

            XDocument xdoc = XDocument.Load(filePath);

            var res = xdoc.Elements();

            var dk = res.Select(item => new NNResult()
            {
               // NNType = item.Element()
            });

            //NeuralNetworkAPI.loadModel(nResult);

            return operation;
        }
    }
}

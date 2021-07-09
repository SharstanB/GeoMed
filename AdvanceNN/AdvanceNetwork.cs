using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.Base.LSTMDTOs;
using Keras.Models;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceNN
{
    public static class AdvanceNetwork
    {
        private static void SetPythonPath()
        {
           var pythonPath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData) , @"Programs\Python\Python38");

           Environment.SetEnvironmentVariable("PATH", $@"{pythonPath};" + Environment.GetEnvironmentVariable("PATH"));
           Environment.SetEnvironmentVariable("PYTHONHOME", pythonPath);
           Environment.SetEnvironmentVariable("PYTHONPATH ", $@"{pythonPath}\Lib");


        }
        private static NDarray ParseToNumpy(IEnumerable<float[][]> sourceList)
        {
            NDarray result = np.empty();
            if (sourceList.Any())
            {
                float[,,] x = new float[sourceList.Count(), sourceList.FirstOrDefault().Length,
                                   sourceList.FirstOrDefault().FirstOrDefault().Length];

                for (int i = 0; i < sourceList.Count(); i++)

                {
                    for (int j = 0; j < sourceList.FirstOrDefault().Length; j++)
                    {
                        var list = sourceList.FirstOrDefault()[j];
                        for (int k = 0; k < list.Length; k++)
                        {
                            x[i, j , k] = list[k];
                        }
                    }
                }
                result = np.array(x);
            }

            return result;

        }

        private static (List<float[][]> train , List<float[][]> test ) GetData(ExecutedData executedData 
            , FeatureCases featureCases)
        {
            
            if (executedData == ExecutedData.all)
            {
                if(featureCases == FeatureCases.All_Features)
                {
                    var allData = COVID19USCountry.GetCountiesLSTMInput<Sample>();

                    return (allData.SelectMany(s => new List<float[][]> {
                    s.Features.Select(d => new float[]
                    {
                      (float)d.Cases,
                      (float)d.MedianAge,
                      (float)d.Population
                    }).ToArray()

                }).ToList(), new List<float[][]>());

                }
                else
                {
                    var Data = COVID19USCountry.
                        GetCountiesLSTMInput<List<float[]>>()
                        .Select(s=>s.ToArray()).ToList();

                    //var dd = Data.Select(a => a.ToArray()).ToArray();
                    return (Data , new List<float[][]>());

                }


            }

           var data =  COVID19USCountry.GetCountiesLSTMInputWithSplit();

            var train_data = data.trainData.SelectMany(s => new List<float[][]> {
                    s.Features.Select(d => new float[]
                    {
                      (float)d.Cases,
                      //(float)d.MedianAge,
                      //(float)d.Population
                    }).ToArray()

                }).ToList();

            var test_data = data.testData.SelectMany(s => new List<float[][]> {
                    s.Features.Select(d => new float[]
                    {
                      (float)d.Cases,
                      //(float)d.MedianAge,
                      //(float)d.Population
                    }).ToArray()

                }).ToList();


            return (train_data, test_data);
        }


        public static ( NDarray trainX , NDarray trainY, (int FD, int SD) inputDimention)  
            GetTrainDataWithDimentions(ExecutedData executedData , FeatureCases featureCases)
        {
            var dataResult = GetData(executedData , featureCases);

            return (ParseToNumpy(dataResult.train), ParseToNumpy(dataResult.test)
                , (dataResult.train.FirstOrDefault().Length, dataResult.train.FirstOrDefault().FirstOrDefault().Length));
        }


        public static void TrainNN(NNType nNType , ExecutedData executedData , FeatureCases featureCases)
        {

            SetPythonPath();
            using (Py.GIL())
            {
                if(nNType == NNType.Conv_LSTM)
                {
                    ConvLSTM_NN.Train_CNN(executedData , featureCases);
                }
                if(nNType == NNType.LSTM)
                {
                    LSTM_NN.TrainLSTM(executedData , featureCases);
                }
                if(nNType == NNType.GRU)
                {
                    GRU_NN.Train_GRU(executedData , featureCases);
                }
            }
        }

        public static void SaveModel( this Sequential sequential , NNType nNType)
        {
            var path = Path.Combine( Environment.CurrentDirectory.
                Substring(0, (Environment.CurrentDirectory.IndexOf("AdvanceNN") + "AdvanceNN".Length )) , "modles", nNType.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string modelsDirectory = Path.Combine( path
                , $"{ DateTime.Now.ToString("yyyy-MM-dd") + DateTime.Now.TimeOfDay.ToString().Replace(":", "-")}.h5");

            sequential.Save(modelsDirectory);

        }


        public static string Forecasting(string path , Sample sample)
        {

            SetPythonPath();

            using (Py.GIL())
            {
                string rv = "";
             
               // string modelPath = Path.GetFullPath(@"modles\GRU\2021-07-0200-50-13.7059485.h5");
                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                var modelPath = Path.Combine(projectDirectory, @"modles\LSTM\2021-07-0822-50-16.0591005.h5");
                //  string modelPath = Path.GetFullPath("model.h5");
                string weightsPath = Path.GetFullPath("weights.h5");

                if (File.Exists(modelPath))
                {
                    var img = GetData(ExecutedData.all , FeatureCases.Only_Cases);
                    NDarray x = ParseToNumpy(img.train);
                    //    NDarray x = ParseToNumpy(new List<float[][]>() {
                    //  sample.Features.Select(d => new float[]
                    //    {
                    //      (float)d.Cases,
                    //      //(float)d.MedianAge,
                    //      //(float)d.Population
                    //    }).ToArray()
                    //});
                    var model = Sequential.LoadModel(modelPath);
                    var y = model.Predict(x);
                }
                else
                {
                    throw (new Exception($"No model found at: { modelPath}"));
                }
                return rv;

            }

        }
    }
}

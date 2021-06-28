using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using GeoMed.NN.Base.Enums;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceNN
{
    public static class AdvanceNetwork
    {
        public static NDarray ParseToNumpy(IEnumerable<float[][]> sourceList)
        {

                float[,,] x = new float[sourceList.Count(), sourceList.FirstOrDefault().Length,
                    sourceList.FirstOrDefault().FirstOrDefault().Length];

                for (int i = 0; i < sourceList.Count(); i++)
                {
                    for (int j = 0; j < sourceList.FirstOrDefault().Length; j++)
                    {
                        var list = sourceList.FirstOrDefault().FirstOrDefault();
                        for (int k = 0; k < list.Length; k++)
                        {
                            x[i, j, k] = list[k];
                        }
                    }
                }
                NDarray result = np.array(x);
                return result;

        }

        public static (List<float[][]> train , List<float[][]> test ) GetData()
        {
            var data = COVID19USCountry.GetCountiesLSTMInput();

            var train_data = data.trainData.SelectMany(s => new List<float[][]> {
                    s.Features.Select(d => new float[]
                    {
                      (float)d.Cases,
                      (float)d.MedianAge,
                      (float)d.Population
                    }).ToArray()

                }).ToList();

            var test_data = data.testData.SelectMany(s => new List<float[][]> {
                    s.Features.Select(d => new float[]
                    {
                      (float)d.Cases,
                      (float)d.MedianAge,
                      (float)d.Population
                    }).ToArray()

                }).ToList();


            return (train_data, test_data);
        }


        public static ( NDarray train , NDarray test , (int FD, int SD) inputDimention)  GetTrainDataWithDimentions()
        {
            var dataResult = GetData();

            return (ParseToNumpy(dataResult.train), ParseToNumpy(dataResult.test)
                , (dataResult.train.FirstOrDefault().Length, dataResult.train.FirstOrDefault().FirstOrDefault().Length));
        }


        public static void TrainNN(NNType nNType)
        {
            var pythonPath = @"C:\Users\sharstan\AppData\Local\Programs\Python\Python38";

            Environment.SetEnvironmentVariable("PATH", $@"{pythonPath};" + Environment.GetEnvironmentVariable("PATH"));
            Environment.SetEnvironmentVariable("PYTHONHOME", pythonPath);
            Environment.SetEnvironmentVariable("PYTHONPATH ", $@"{pythonPath}\Lib");


            using (Py.GIL())
            {
                if(nNType == NNType.Conv_LSTM)
                {
                    CNN_Model.Train_CNN();
                }
                if(nNType == NNType.LSTM)
                {
                    LSTM_NN.TrainLSTM();
                }
            }
        }

    }
}

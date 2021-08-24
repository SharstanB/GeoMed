using System;
using System.Collections.Generic;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork;
using Microsoft.Extensions.DependencyInjection;

namespace AdvanceNN
{
    class Program
    {
        static void Main(string[] args)
        {


            //var hh = new float[][] {
            //    new float[] { 0.98300f , 0.98415f, 0.98529f,0.98682f,0.99159f , 0.99255f,0.994272f}
            //};
            //var data = AdvanceNetwork.GetData(ExecutedData.all, FeatureCases.Only_Cases);

            //var trainX_data_numpy = data.train[0];
            //var list = new List<float[][]>() { trainX_data_numpy };

            //var hh1 = new float[][] { new float[] { 40f } };
            //var hh2 = new float[][] { new float[] { 100f } };
            //var hh3 = new float[][] { new float[] { 150f } };
            //var hh4 = new float[][] { new float[] { 200f } };
            //var hh5 = new float[][] { new float[] { 250f } };
            //var hh6 = new float[][] { new float[] { 300f } };
            //var hh7 = new float[][] { new float[] { 350f} };
            //var list = new List<float[][]>() { hh1 , hh2 , hh3 , hh4, hh5 , hh6 , hh7 };

            //var list = new List<float[][]>
            //    (new float[][](new float[] { 0.98300f },
            //        new float[] { 0.98415f }, new float[] { 0.98529f },
            //        new float[] { 0.98682f }, new float[] { 0.99159f },
            //        new float[] { 0.99255f }, new float[] { 0.994272f } });

            //var dd = AdvanceNetwork.Forecasting("", list);

            //new GeoMed.NN.Base.LSTMDTOs.Feature()
            //{
            //    Cases = 1,
            //    //MedianAge = 0.32493,
            //    //Population = 0.123
            //},
            //       new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 4,
            //           //MedianAge = 0.32493,
            //           //Population = 0.123
            //       },
            //       new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 1,
            //           //MedianAge = 0.32493,
            //           //Population = 0.123
            //       },
            //       new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 0,
            //           //MedianAge = 0.32493,
            //           //Population = 0.123
            //       },
            //       new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 0,
            //           //MedianAge = 0.32493,
            //           //Population = 0.123
            //       } ,new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 0,
            //           //MedianAge = 0.32493,
            //           //Population = 0.123
            //       },new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 2,
            //           //MedianAge = 0.32493,
            //           //Population = 0.123
            //       },new GeoMed.NN.Base.LSTMDTOs.Feature()
            //       {
            //           Cases = 0,
            //           //MedianAge = 0.32493,



            AdvanceNetwork.TrainNN(NNType.GRU, ExecutedData.all, FeatureCases.Only_Cases);

           // var serviceProvider = new ServiceCollection()
           //.AddSingleton<INNRepository, NNRepository>()
           //.BuildServiceProvider();

           // var epochs = 1;


           // var result = NeuralNetworkAPI.
           //     GetNetworkResult(epochs: epochs, errorRate: 0.001f, executedData: ExecutedData.County, hiddenCount: 4, NNType.BackProbagation);


           // Print($"\n Network Error : {result.NetworkError} " , ConsoleColor.DarkBlue);

           // Print($"\n Epochs : {epochs} ", ConsoleColor.Green);

           // Print($"\n Training Samples  : ", ConsoleColor.Red);

           
           // result.TrainSamples.ForEach(sample =>
           // {
           //     int i = 1;

           //     Print($"\n ---------------------------------------------------------------------------------- Epoch : {sample.epoch} \n", ConsoleColor.Yellow );


           //     Print($"\n Error : {sample.error} \n", ConsoleColor.Cyan);


           //     sample.samples.ForEach(sample =>
           //     {
           //         Print($"sample {i++} actaul output : {sample.ActualOutput} "
           //       , ConsoleColor.Magenta);

           //         Print($" - target output : {sample.TargetOutput} "
           //            , ConsoleColor.Cyan);

           //         Print($" -  error : {sample.NeuronError} \n"
           //            , ConsoleColor.Green);
           //     });
              
           // });


           // Print($" Testing Samples  : " , ConsoleColor.Red);

           // var i = 1;
           // result.TestSamples.ForEach(sample =>
           // {
           //     Print($"\n sample {i++} actaul output : {sample.ActualOutput} " 
           //         , ConsoleColor.Magenta);

           //     Print($" - target output : {sample.TargetOutput} "
           //        , ConsoleColor.Cyan);

           //     Print($" -  error : {sample.NeuronError} \n"
           //        , ConsoleColor.Green);
           // });


           // Print($"Final Weigths : \n" , ConsoleColor.Red);

           // result.FinalWeigths.ForEach(item =>
           // {
           //     Print($" Layer Type : {item.LayerType} \n" , ConsoleColor.Blue);

           //     if(item.LayerType == GeoMed.NN.Base.Enums.LayerType.Hidden)
           //     {
           //         Print($" - HiddenNumber {item.HiddenNumber}  \n" , ConsoleColor.Green);
           //     }
              
           //     item.weigths.ForEach(weigths =>
           //     {
           //         weigths.ForEach(weigth =>
           //         {
           //             Print($"{weigth} ");
           //         });
           //     });
           // });

           // serviceProvider.GetService<INNRepository>().SaveTrainedModel(result);
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

using System;
using System.Collections.Generic;
using GeoMed.Main.Data.Repositories;
using GeoMed.Main.IData.IRepositories;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork;
using GeoMed.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace AdvanceNN
{
    class Program
    {
        static void Main(string[] args)
        {


            //var hh = new float[][] {
            //    new float[] { 4 }
            //};
            //var list = new List<float[][]>() { hh };

            ////var hh = new float[] { 4.0f };
            ////var list = new List<float[]>() { hh };

            ////var list = new List<float[][]>(new float[][] { new float[] { 0 }, new float[] { 1 }, new float[] { 4 }, new float[] { 1 }, new float[] { 0 }, new float[] { 0 }, new float[] { 0 }, new float[] { 2 }, new float[] { 0 } });

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


            AdvanceNetwork.TrainNN(NNType.Conv, ExecutedData.all, FeatureCases.Only_Cases);

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

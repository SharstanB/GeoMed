using System;
using GeoMed.Main.Data.Repositories;
using GeoMed.Main.IData.IRepositories;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork;
using Microsoft.Extensions.DependencyInjection;

namespace TestNN
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
           .AddSingleton<IMapRepository, MapRepository>()
           .BuildServiceProvider();

            var epochs = 1;
            //var result = NeuralNetworkAPI.GetNetworkResult(epochs: epochs, errorRate: 0.003f, executedData: ExecutedData.County, hiddenCount: 7);

            var result = NeuralNetworkAPI.
                GetElmanNetworkResult(epochs: epochs, errorRate: 0.001f, executedData: ExecutedData.County, hiddenCount: 4);


            Print($"\n Network Error : {result.NetworkError} " , ConsoleColor.DarkBlue);

            Print($"\n Epochs : {epochs} ", ConsoleColor.Green);

            Print($"\n Training Samples  : ", ConsoleColor.Red);

           
            result.TrainSamples.ForEach(sample =>
            {
                int i = 1;

                Print($"\n ---------------------------------------------------------------------------------- Epoch : {sample.epoch} \n", ConsoleColor.Yellow );


                Print($"\n Error : {sample.error} \n", ConsoleColor.Cyan);


                sample.samples.ForEach(sample =>
                {
                    Print($"sample {i++} actaul output : {sample.ActualOutput} "
                  , ConsoleColor.Magenta);

                    Print($" - target output : {sample.TargetOutput} "
                       , ConsoleColor.Cyan);

                    Print($" -  error : {sample.NeuronError} \n"
                       , ConsoleColor.Green);
                });
              
            });


            Print($" Testing Samples  : " , ConsoleColor.Red);

            var i = 1;
            result.TestSamples.ForEach(sample =>
            {
                Print($"\n sample {i++} actaul output : {sample.ActualOutput} " 
                    , ConsoleColor.Magenta);

                Print($" - target output : {sample.TargetOutput} "
                   , ConsoleColor.Cyan);

                Print($" -  error : {sample.NeuronError} \n"
                   , ConsoleColor.Green);
            });


            Print($"Final Weigths : \n" , ConsoleColor.Red);

            result.FinalWeigths.ForEach(item =>
            {
                Print($" Layer Type : {item.LayerType} \n" , ConsoleColor.Blue);

                if(item.LayerType == GeoMed.NN.Base.Enums.LayerType.Hidden)
                {
                    Print($" - HiddenNumber {item.HiddenNumber}  \n" , ConsoleColor.Green);
                }
              
                item.weigths.ForEach(weigths =>
                {
                    weigths.ForEach(weigth =>
                    {
                        Print($"{weigth} ");
                    });
                });
            });

            serviceProvider.GetService<IMapRepository>().SaveTrainedModel(result);
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

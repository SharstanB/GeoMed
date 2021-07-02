using System;
using GeoMed.Main.Data.Repositories;
using GeoMed.Main.IData.IRepositories;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork;
using Microsoft.Extensions.DependencyInjection;

namespace AdvanceNN
{
    class Program
    {
        static void Main(string[] args)
        {



            AdvanceNetwork.Forecasting("", new GeoMed.NN.Base.LSTMDTOs.Sample()
            {
                Features = new System.Collections.Generic.List<GeoMed.NN.Base.LSTMDTOs.Feature>()
                {
                   new GeoMed.NN.Base.LSTMDTOs.Feature()
                   {
                       Cases = 0.000000323,
                       MedianAge = 0.32493,
                       Population = 0.123
                   }
                }
            });



            AdvanceNetwork.TrainNN(NNType.GRU, ExecutedData.splited);

            var serviceProvider = new ServiceCollection()
           .AddSingleton<INNRepository, NNRepository>()
           .BuildServiceProvider();

            var epochs = 1;


            var result = NeuralNetworkAPI.
                GetNetworkResult(epochs: epochs, errorRate: 0.001f, executedData: ExecutedData.County, hiddenCount: 4, NNType.BackProbagation);


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

            serviceProvider.GetService<INNRepository>().SaveTrainedModel(result);
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

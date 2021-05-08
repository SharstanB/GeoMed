using System;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork;
namespace TestNN
{
    class Program
    {
        static void Main(string[] args)
        {

            var epochs = 5;
           // var result = NeuralNetworkAPI.GetNetworkResult(epochs: 10, errorRate: 0.003f , hiddenCount: 7);

            var result = NeuralNetworkAPI.
                GetElmanNetworkResult(epochs: epochs,  errorRate: 0.003f , executedData: ExecutedData.State , hiddenCount: 7);


            Print($"\n Network Error : {result.NetworkError} " , ConsoleColor.DarkBlue);

            Print($"\n Epochs : {epochs} ", ConsoleColor.Green);

            Print($"\n Training Samples  : ", ConsoleColor.Red);

           
            result.TrainSamples.ForEach(sample =>
            {
                int i = 1;

                Print($"\n ---------------------------------------------------------------------------------- Epoch : {sample.epoch} \n", ConsoleColor.Yellow );

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
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

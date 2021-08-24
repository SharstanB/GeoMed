using GeoMed.NN.Base.Enums;
using Keras;
using Keras.Layers;
using Keras.Models;
using Keras.Optimizers;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceNN
{
    public static class GRU_NN
    {
        
        public static void Train_GRU(ExecutedData executedData , FeatureCases featureCases)
        {
            Parallel.Invoke(() =>
            {
                var data = AdvanceNetwork.GetTrainDataWithDimentions(executedData , featureCases);

                var trainX_data_numpy = data.trainX;

                var trainY_data_numpy = data.trainY;
                
                var model = new Sequential();

                //model.Add(new GRU(units: 128, return_sequences: true,
                //       activation: "relu"));
                //model.Add(new Dropout(0.5));

                //model.Add(new GRU(64, activation: "relu", return_sequences: true));

                //model.Add(new Dropout(0.5));

                model.Add(new GRU(32, activation: "relu", return_sequences: true));

                model.Add(new Dense(8, activation: "relu"));

                model.Add(new Dropout(0.5));

                model.Add(new Dense(1, activation: "relu"));

                var sgd = new SGD(0.001f);
                var adam = new Adam(0.001f, 0.000001f);
                model.Compile(optimizer: adam  , loss: "mean_absolute_error", metrics: new string[] { "mse" });

                var result = model.Fit(trainX_data_numpy,
                    trainY_data_numpy, batch_size: 1, epochs: 20 , verbose: 1 , validation_split:0.2f);

                model.SaveModel(NNType.GRU);

                dynamic mpl = Py.Import("matplotlib");
                mpl.use("TkAgg");
                dynamic plt_loss = Py.Import("matplotlib.pyplot");
                dynamic plt_accuracy = Py.Import("matplotlib.pyplot");
                var loss = result.HistoryLogs["loss"].Select(s => (float)s).ToList();
                var accuracy = result.HistoryLogs["mse"].Select(s => (float)s).ToList();
                var val_loss = result.HistoryLogs["val_loss"].Select(s => (float)s).ToList();
                var val_accuracy = result.HistoryLogs["val_mse"].Select(s => (float)s).ToList();

                plt_loss.plot(loss);
                plt_loss.plot(val_loss);
                plt_loss.show();
                plt_loss.figure();
                plt_accuracy.plot(accuracy);
                plt_accuracy.plot(val_accuracy);
                plt_accuracy.show();

            });
            
        }
    }
}

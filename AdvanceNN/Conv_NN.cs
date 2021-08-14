using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using GeoMed.NN.Base.Enums;
using Keras;
using Keras.Layers;
using Keras.Models;
using Keras.Optimizers;
using Numpy;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace AdvanceNN
{
    public static class Conv_NN
    {

        public static void Train_CNN(ExecutedData executedData , FeatureCases featureCases)
        {
           
                var data = AdvanceNetwork.GetTrainDataWithDimentions(executedData , featureCases);

                var trainX_data_numpy = data.trainX;

                var trainY_data_numpy = data.trainY;

                //Build sequential model
                var model = new Sequential();
            var shape = trainX_data_numpy.shape;

                model.Add(new Conv1D(32, kernel_size: 3
                    , activation: "relu", input_shape: new Shape(
                    data.inputDimention.FD,
                    data.inputDimention.SD
                    )
                    ));

            model.Add(new MaxPooling1D(pool_size: 2));
            model.Add(new Dropout(0.5));
            model.Add(new Conv1D(16, kernel_size: 3, activation: "relu"));
            //model.Add(new MaxPooling1D(pool_size: 2));
            model.Add(new Dropout(0.5));
            model.Add(new Flatten());
            model.Add(new Dense(1, activation:"linear"));
            var sgd = new SGD(0.001f);
            model.Compile(optimizer: sgd, loss: "mean_absolute_error", metrics: new string[] { "mse" });
            var result = model.Fit(data.trainX,data.trainY
                  , batch_size: 1, epochs: 100, verbose: 1 , validation_split: 0.2f);


            //string json = model.ToJson();
            //File.WriteAllText("model.json", json);
            model.SaveModel(NNType.Conv);



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
           
        }
    }
}

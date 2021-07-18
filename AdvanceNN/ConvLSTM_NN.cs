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
    public static class ConvLSTM_NN
    {

        public static void Train_CNN(ExecutedData executedData , FeatureCases featureCases)
        {
           
                var data = AdvanceNetwork.GetTrainDataWithDimentions(executedData , featureCases);

                var trainX_data_numpy = data.trainX;

                var trainY_data_numpy = data.trainY;

                //Build sequential model
                var model = new Sequential();
            var shape = trainX_data_numpy.shape;

                model.Add(new Conv1D(8, kernel_size: 3
                    , activation: "softplus", input_shape: new Shape(
                    data.inputDimention.FD,
                    data.inputDimention.SD
                    )
                    ));
            model.Add(new MaxPooling1D(pool_size: 2));
            model.Add(new Dropout(0.2));
            model.Add(new LSTM(16, activation: "softplus", return_sequences: true));
            //  model.Add(new Conv1D(filters: 16, kernel_size: 3, activation: "sigmoid"));
            model.Add(new Flatten());
            model.Add(new Dropout(0.2));
            //model.Add(new Dense(100, activation: "relu"));
            model.Add(new Dense(1, activation:"linear"));
            //model.Add(new Dense(1800));
            //model.Add(new LSTM(32, activation: "sigmoid", return_sequences: true));
            //model.Add(new Dropout(0.2));
            //model.Add(new LSTM(32, activation: "sigmoid", return_sequences: true));
          //  model.Add(new Dense(1));

            //Compile and train
            //model.Compile(optimizer: "sgd", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });
           // var sgd = new SGD(0.0001f, 0.0f, 0.0f, false);
            model.Compile(optimizer: "adam", loss: "mse", metrics: new string[] { "accuracy" });

              var result =  model.Fit(trainX_data_numpy,
                    (executedData == ExecutedData.all) ? trainX_data_numpy : trainY_data_numpy, batch_size: 1, epochs: 10, verbose: 1);

            dynamic mpl = Py.Import("matplotlib");
            mpl.use("TkAgg");
            dynamic plt_loss = Py.Import("matplotlib.pyplot");
            dynamic plt_accuracy = Py.Import("matplotlib.pyplot");
            var loss = result.HistoryLogs["loss"].Select(s => (float)s).ToList();
            var accuracy = result.HistoryLogs["accuracy"].Select(s => (float)s).ToList();
            plt_loss.plot(loss);
            plt_loss.show();
            plt_loss.figure();
            plt_accuracy.plot(accuracy);
            plt_accuracy.show();
            //Save model and weights
            string json = model.ToJson();
              File.WriteAllText("model.json", json);
                
                
               // model.SaveWeight("model.h5");
                model.SaveModel(NNType.Conv_LSTM);

                //Load model and weight
                //var loaded_model = Sequential.ModelFromJson(File.ReadAllText("model.json"));
                //loaded_model.LoadWeight("model.h5");
        }
    }
}

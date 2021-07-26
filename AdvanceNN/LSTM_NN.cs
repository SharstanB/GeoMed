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
    public static class LSTM_NN
    {
        public static void TrainLSTM(ExecutedData executedData , FeatureCases featureCases)
        {

          

            var data = AdvanceNetwork.GetTrainDataWithDimentions(executedData , featureCases);

                var trainx_data_numpy = data.trainX;


                var trainY_data_numpy = data.trainY;
            

                //Build sequential model
                var model = new Sequential();
                
                model.Add(new LSTM(64 , activation: "relu", input_shape: new Shape(
                    data.inputDimention.FD,
                    data.inputDimention.SD) 
                    , return_sequences: true
                    ));
                model.Add(new Dropout(0.2));
                model.Add(new LSTM(32, activation: "relu", return_sequences: true));
                model.Add(new Dropout(0.2));
                model.Add(new LSTM(32, activation: "relu", return_sequences: true));
            //    model.Add(new Dropout(0.2));
            //model.Add(new LSTM(16, activation: "sigmoid", return_sequences: true));
            //model.Add(new Dropout(0.2));
            //model.Add(new LSTM(16, activation: "sigmoid", return_sequences: true));
            //model.Add(new Dropout(0.5));

            //model.Add(new LSTM(16, activation: "sigmoid", return_sequences: true));
            model.Add(new Dense(1, activation: "linear"));

            //Compile and train
            //model.Compile(optimizer: "sgd", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });

              var sgd = new SGD(0.01f, 0.0f, 0.0f, false);

                model.Compile(optimizer: "adam", loss: "mse", metrics: new string[] { "accuracy" });

             var result =  model.Fit(trainx_data_numpy,
                  (executedData == ExecutedData.all)? trainx_data_numpy :
                    trainY_data_numpy, batch_size: 1, 
                    epochs: 100, verbose: 1 , validation_split:0.2f);


            dynamic mpl = Py.Import("matplotlib");
            mpl.use("TkAgg");
            dynamic plt_loss = Py.Import("matplotlib.pyplot");
            dynamic plt_accuracy = Py.Import("matplotlib.pyplot");
            var loss = result.HistoryLogs["loss"].Select(s => (float)s).ToList();
            var accuracy = result.HistoryLogs["accuracy"].Select(s => (float)s).ToList();
            var val_loss = result.HistoryLogs["val_loss"].Select(s => (float)s).ToList();
            var val_accuracy = result.HistoryLogs["val_accuracy"].Select(s => (float)s).ToList();
            plt_loss.plot(loss);
            plt_loss.plot(val_loss);
            plt_loss.show();
            plt_loss.figure();
            plt_accuracy.plot(accuracy);
            plt_accuracy.plot(val_accuracy);
            plt_accuracy.show();

            //Save model and weights
            string json = model.ToJson();
                File.WriteAllText("model.json", json);

                
                model.SaveModel(NNType.LSTM);

           
            ////Load model and weight
            //var loaded_model = Sequential.ModelFromJson(File.ReadAllText("model.json"));
            //loaded_model.LoadWeight("model.h5");

        }
       
    }
}

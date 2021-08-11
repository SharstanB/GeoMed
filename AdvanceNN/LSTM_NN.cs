﻿using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
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

             var trainx_data_numpy = data.trainX.reshape(218 , 201 , 1 );


             var trainY_data_numpy = data.trainY.reshape(218, 201, 1);

            
            var train = new Keras.PreProcessing
                .sequence.TimeseriesGenerator(data: trainx_data_numpy,
             targets: trainx_data_numpy, length: 201, sampling_rate: 1, stride: 1, batch_size: 3);


            var model = new Sequential();
                
                model.Add(new LSTM(64, activation: "sigmoid", input_shape: new Shape(
                    data.inputDimention.FD,
                    data.inputDimention.SD) 
                    , return_sequences: true
                    ));
            model.Add(new Dropout(0.2));
            model.Add(new LSTM(32, activation: "relu", return_sequences: true));
            model.Add(new Dropout(0.2));
            model.Add(new Dense(8, activation: "sigmoid"));
            model.Add(new Dropout(0.2));
            model.Add(new Dense(1 ));

            var sgd = new SGD(0.01f);
            model.Compile(optimizer: "adam", loss: "mean_squared_error", metrics: new string[] { "mse" });

            var result = model.Fit(trainx_data_numpy,
                   trainY_data_numpy, batch_size: 1,
                   epochs: 15 , verbose: 1, validation_split: 0.4f);


            //   var result = model.FitGenerator(train, epochs: 20);

            model.SaveModel(NNType.LSTM);


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

            //Save model and weights
            string json = model.ToJson();
                File.WriteAllText("model.json", json);

             
           
            ////Load model and weight
            //var loaded_model = Sequential.ModelFromJson(File.ReadAllText("model.json"));
            //loaded_model.LoadWeight("model.h5");

        }
       
    }
}

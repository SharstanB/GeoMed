using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using Keras;
using Keras.Layers;
using Keras.Models;
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
        public static void TrainLSTM()
        {

                var data = AdvanceNetwork.GetTrainDataWithDimentions();

                var train_data_numpy = data.train;


               // TODO  need review
                var test_data_numpy = data.test;   

               // var data_list = tf.stack(data_list)
               //  y = tf.stack(y)

                //Build sequential model
                var model = new Sequential();
                
                model.Add(new LSTM(128 , activation: "relu", input_shape: new Shape(
                    //train_data.Count, 
                    data.inputDimention.FD,
                    data.inputDimention.SD) 
                    , return_sequences: true
                    //, return_state: true
                    ));

                model.Add(new LSTM(128, activation: "relu", return_sequences: true));
                model.Add(new Dense(1, activation: "linear"));

                //Compile and train
                //model.Compile(optimizer: "sgd", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });

                model.Compile(optimizer: "rmsprop", loss: "mean_squared_error", metrics: new string[] { "accuracy" });

                model.Fit(train_data_numpy, train_data_numpy, batch_size: 1, epochs: 10, verbose: 1);

                //Save model and weights
                string json = model.ToJson();
                File.WriteAllText("model.json", json);
                model.SaveWeight("model.h5");

                //Load model and weight
                var loaded_model = Sequential.ModelFromJson(File.ReadAllText("model.json"));
                loaded_model.LoadWeight("model.h5");

        }
       
    }
}

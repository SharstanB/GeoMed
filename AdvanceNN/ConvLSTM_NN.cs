using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using GeoMed.NN.Base.Enums;
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
    public static class ConvLSTM_NN
    {

        public static void Train_CNN(ExecutedData executedData , FeatureCases featureCases)
        {
           
                var data = AdvanceNetwork.GetTrainDataWithDimentions(executedData , featureCases);

                var trainX_data_numpy = data.trainX;

                var trainY_data_numpy = data.trainY;

                //Build sequential model
                var model = new Sequential();

                model.Add(new Conv1D(128, kernel_size: 5 , strides: 1
                    , activation: "sigmoid", padding: "causal", input_shape: new Shape(
                    data.inputDimention.FD,
                    data.inputDimention.SD)
                    ));

                model.Add(new LSTM(128, activation: "sigmoid", return_sequences: true));
                model.Add(new LSTM(128, activation: "sigmoid", return_sequences: true));
                model.Add(new Dense(1, activation: "linear"));

                //Compile and train
                //model.Compile(optimizer: "sgd", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });

                model.Compile(optimizer: "rmsprop", loss: "mean_squared_error", metrics: new string[] { "accuracy" });

                model.Fit(trainX_data_numpy,
                    (executedData == ExecutedData.all) ? trainX_data_numpy : trainY_data_numpy, batch_size: 1, epochs: 10, verbose: 1);

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

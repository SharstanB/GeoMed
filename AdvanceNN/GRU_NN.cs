using GeoMed.NN.Base.Enums;
using Keras;
using Keras.Layers;
using Keras.Models;
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

                //Build sequential model
                var model = new Sequential();

                model.Add(new GRU(units: 128, return_sequences: true,
                       activation: "relu"));
                model.Add(new Dropout(0.2));

                model.Add(new GRU(128, activation: "relu", return_sequences: true));

                model.Add(new Dropout(0.2));

                model.Add(new GRU(128, activation: "relu", return_sequences: true));

                model.Add(new Dense(1, activation: "linear"));

                //Compile and train
                //model.Compile(optimizer: "sgd", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });

                model.Compile(optimizer: "adam", loss: "mean_squared_error", metrics: new string[] { "accuracy" });

                model.Fit(trainX_data_numpy,
                    //(executedData == ExecutedData.all) ? trainX_data_numpy : 
                    trainY_data_numpy, batch_size: 1, epochs: 50 , verbose: 1);

              //  model.Fit(trainX_data_numpy, trainX_data_numpy, batch_size: 1, epochs: 10, verbose: 1);


                //Save model and weights
                string json = model.ToJson();
                File.WriteAllText("model.json", json);


                // model.SaveWeight("model.h5");
                model.SaveModel(NNType.GRU);

                //Load model and weight
                //var loaded_model = Sequential.ModelFromJson(File.ReadAllText("model.json"));
                //loaded_model.LoadWeight("model.h5");
            });
            
        }
    }
}

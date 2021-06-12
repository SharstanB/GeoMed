using GeoMed.Base;
using GeoMed.Main.DTO.Settings;
using GeoMed.NN.BPNeuralNetwork.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Main.IData.IRepositories
{
    public interface INNRepository
    {
        OperationResult<bool> SaveTrainedModel(NNResult nNResult);

        OperationResult<NNResult> TrainNeuralNetwork(TrainNeuralNetworkDto trainNeural);

        public OperationResult<NNResult> LoadModel();




    }
}

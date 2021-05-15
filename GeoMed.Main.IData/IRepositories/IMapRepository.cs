using GeoMed.NN.BPNeuralNetwork.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Main.IData.IRepositories
{
    public interface IMapRepository
    {
        void SaveTrainedModel(NNResult nNResult);

        
    }
}

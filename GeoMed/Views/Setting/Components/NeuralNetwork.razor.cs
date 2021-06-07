using GeoMed.Main.DTO.Settings;
using GeoMed.Main.IData.IRepositories;
using GeoMed.Util;
using GeoMed.ViewModels.Settings;
using Microsoft.AspNetCore.Components;

namespace GeoMed.Views.Setting.Components
{
    partial class NeuralNetwork
    {
        TrainNNViewModel TrainNNVM = new TrainNNViewModel();

        [Inject]
        private INNRepository INNRepository { get; set; }
        public void OnSubmit()
        {
            var mapper = Mapper.TrainNNMapper.CreateMapper();
            INNRepository.TrainNeuralNetwork(mapper.Map<TrainNeuralNetworkDto>(TrainNNVM));
        }

    }
}

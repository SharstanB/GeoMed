using GeoMed.NN.Base.Enums;

namespace GeoMed.Main.DTO.Settings
{
    public class TrainNeuralNetworkDto
    {
        public int Epochs { get; set; }

        public float ErrorRate { get; set; }

        public int HiddenLayersCount { get; set; }

        public ExecutedData ExecutedData { get; set; }

        public NNType NNType { get; set; }
    }
}

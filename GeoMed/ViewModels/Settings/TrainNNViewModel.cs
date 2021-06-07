using GeoMed.NN.Base.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.ViewModels.Settings
{
    public class TrainNNViewModel
    {

        [Range(1, 100, ErrorMessage = "المجال بين 1 و 100")]
        public int? Epochs { get; set; }

        [Range(0 , 1 , ErrorMessage="يجب أن تكون قيمة عشرية")]
        public float? LearningRate { get; set; }

        [Range(1, 10, ErrorMessage = "المجال بين 1 و 10")]
        public int? HiddenLayersCount { get; set; }

        public ExecutedData ExecutedData { get; set; }

        public NNType NNType { get; set; }
    }
}

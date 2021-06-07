using AutoMapper;
using GeoMed.Main.DTO.Settings;
using GeoMed.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Util
{
    public static class Mapper
    {
        public static MapperConfiguration TrainNNMapper  = new MapperConfiguration(cfg => {
            cfg.CreateMap<TrainNNViewModel, TrainNeuralNetworkDto>();
        });

    }
}

using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using System;

namespace GetADoctor.Web.Infrastructure.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config => 
            {
                config.CreateMap<StateViewModel, State>();
                config.CreateMap<State, StateViewModel>();

                config.CreateMap<CityViewModel, City>();
                config.CreateMap<City, CityViewModel>();


            });
        }
    }
}
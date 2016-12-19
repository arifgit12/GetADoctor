using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using GetADoctor.Web.Models;
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

                config.CreateMap<SpecialityViewModel, Speciality>();
                config.CreateMap<Speciality, SpecialityViewModel>();

                config.CreateMap<DoctorViewModel, Doctor>();
                config.CreateMap<Doctor, DoctorViewModel>();

                config.CreateMap<PatientViewModel, Patient>();
                config.CreateMap<Patient, PatientViewModel>();

                config.CreateMap<AddressViewModel, Location>();
                config.CreateMap<Location, AddressViewModel>();

                config.CreateMap<ScheduleViewModel, Schedule>();
                config.CreateMap<Schedule, ScheduleViewModel>();

            });
        }
    }
}
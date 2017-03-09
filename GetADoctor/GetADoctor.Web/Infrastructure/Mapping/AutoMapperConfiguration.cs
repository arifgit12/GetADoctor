using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using GetADoctor.Web.Models;
using GetADoctor.Web.Models.Specialities;
using System;
using System.Linq;

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
                config.CreateMap<Doctor, DoctorViewModel>()
                     .ForMember(d => d.ImageUrl,
                        opt => opt.MapFrom(x => x.User.ProfilePicUrl));

                config.CreateMap<PatientViewModel, Patient>();
                config.CreateMap<Patient, PatientViewModel>();

                config.CreateMap<AddressViewModel, Location>();
                config.CreateMap<Location, AddressViewModel>();

                config.CreateMap<ScheduleViewModel, Schedule>();
                config.CreateMap<Schedule, ScheduleViewModel>();

                config.CreateMap<Doctor, HomeDoctorViewModel>()
                    .ForMember(d => d.Id,
                        opt => opt.MapFrom(x => x.DoctorId))
                    .ForMember(d => d.FullName,
                        opt => opt.MapFrom(x => x.FirstName + " " + x.LastName))
                     .ForMember(d => d.Rating,
                        opt => opt.MapFrom(x => x.Rating.Count > 0
                        ? (float)x.Rating.Sum(r => r.Value) / x.Rating.Count : 0))
                    .ForMember(d => d.RatingsCount,
                        opt => opt.MapFrom(d => d.Rating.Count))
                    .ForMember(d => d.CommentsCount,
                        opt => opt.MapFrom(d => d.Comments.Count))
                    .ForMember(d => d.City,
                        opt => opt.MapFrom(d => d.User.locations.FirstOrDefault().City));

                config.CreateMap<Doctor, DoctorSearchViewModel>()
                    .ForMember(d => d.FullName, opt => opt.MapFrom(d => d.FirstName + " " + d.LastName));
                //.ForMember(d => d.CityName, opt => opt.MapFrom(d => d.City.Name));

                config.CreateMap<Doctor, SpecialityDoctorViewModel>()
                    .ForMember(d => d.FullName,
                        opt => opt.MapFrom(x => x.FirstName + " " + x.LastName))
                   .ForMember(d => d.Rating,
                        opt => opt.MapFrom(x => x.Rating.Count > 0
                            ? (float)x.Rating.Sum(r => r.Value) / x.Rating.Count : 0))
                   .ForMember(d => d.CommentsCount,
                        opt => opt.MapFrom(x => x.Comments.Count))
                   .ForMember(d => d.Id,
                        opt => opt.MapFrom(x => x.DoctorId));

                config.CreateMap<Appointment, AppointmentViewModel>();
                config.CreateMap<AppointmentViewModel, Appointment>();

                config.CreateMap<Speciality, HomeSpecialityViewModel>();

            });
        }
    }
}
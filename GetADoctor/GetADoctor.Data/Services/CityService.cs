using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;

namespace GetADoctor.Data.Services
{
    public interface ICityService
    {
        IEnumerable<City> GetCities();
        City GetCity(int id);
        int SaveCity(City city);
    }
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }
   
        public IEnumerable<City> GetCities()
        {
            return this.cityRepository.GetAll();
        }

        public City GetCity(int id)
        {
            return this.cityRepository.Get(id);
        }

        public int SaveCity(City city)
        {
            this.cityRepository.Add(city);
            return this.cityRepository.SaveChanges();
        }
    }
}

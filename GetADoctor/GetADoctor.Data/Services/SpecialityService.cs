using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface ISpecialityService
    {
        IEnumerable<Speciality> GetSpecialities();
        Speciality GetSpeciality(int id);
        int SaveSpeciality(Speciality speciality);
    }
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepo;

        public SpecialityService(ISpecialityRepository specialityRepository)
        {
            this._specialityRepo = specialityRepository;
        }

        public IEnumerable<Speciality> GetSpecialities()
        {
            return this._specialityRepo.GetAll();
        }

        public Speciality GetSpeciality(int id)
        {
            return this._specialityRepo.Get(id);
        }

        public int SaveSpeciality(Speciality speciality)
        {
            this._specialityRepo.Add(speciality);
            return this._specialityRepo.SaveChanges();
        }
    }
}

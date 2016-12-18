using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface IProfileService
    {
        Doctor GetDoctorByUserId(string userId);
        Doctor GetDoctor(int id);
        int GetDoctorId(string userId);
        int SaveDoctor(Doctor doctor);
        int UpdateDoctor(Doctor doctor);

        Patient GetPatientByUserId(string userId);
        Patient GetPatient(int id);
        int GetPatientId(string userId);
        int SavePatient(Patient patient);
        int UpdatePatient(Patient patient);

        IEnumerable<Speciality> GetSpecialities();
    }
    public class ProfileService : IProfileService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ISpecialityRepository _specialityRepository;

        public ProfileService(IDoctorRepository doctorRepository, IPatientRepository patientRepository,
            ISpecialityRepository specialityRepository)
        {
            this._doctorRepository = doctorRepository;
            this._patientRepository = patientRepository;
            this._specialityRepository = specialityRepository;
        }

        public Doctor GetDoctor(int id)
        {
            return this._doctorRepository.Get(id);
        }

        public int GetDoctorId(string userId)
        {
            return this._doctorRepository.SearchFor(d => d.UserId == userId).FirstOrDefault().DoctorId;
        }
        
        public Doctor GetDoctorByUserId(string userId)
        {
            return this._doctorRepository.SearchFor(d => d.UserId == userId).FirstOrDefault();
        }

        public int SaveDoctor(Doctor doctor)
        {
            this._doctorRepository.Add(doctor);
            return this._doctorRepository.SaveChanges();
        }

        public int UpdateDoctor(Doctor doctor)
        {
            this._doctorRepository.Update(doctor);
            return this._doctorRepository.SaveChanges();
        }

        public Patient GetPatientByUserId(string userId)
        {
            return this._patientRepository.SearchFor(p => p.UserId == userId).FirstOrDefault();
        }

        public Patient GetPatient(int id)
        {
            return this._patientRepository.Get(id);
        }

        public int GetPatientId(string userId)
        {
            return this._patientRepository.SearchFor(p => p.UserId == userId).FirstOrDefault().PatientId;
        }

        public int SavePatient(Patient patient)
        {
            this._patientRepository.Add(patient);
            return this._patientRepository.SaveChanges();
        }

        public int UpdatePatient(Patient patient)
        {
            this._patientRepository.Update(patient);
            return this._patientRepository.SaveChanges();
        }

        public IEnumerable<Speciality> GetSpecialities()
        {
            return this._specialityRepository.GetAll();
        }
    }
}

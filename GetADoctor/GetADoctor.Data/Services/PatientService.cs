using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface IPatientservice
    {
        IEnumerable<Patient> GetPatients();
        Patient GetPatientByUserId(string userId);
        Patient GetPatient(int id);
        int GetPatientId(string userId);
        int SavePatient(Patient patient);
    }
    public class PatientService : IPatientservice
    {
        private readonly IPatientRepository _patientRepo;

        public PatientService(IPatientRepository patientRepository)
        {
            this._patientRepo = patientRepository;
        }

        public IEnumerable<Patient> GetPatients()
        {
            return this._patientRepo.GetAll();
        }

        public Patient GetPatient(int id)
        {
            return this._patientRepo.Get(id);
        }

        public int GetPatientId(string userId)
        {
            return this._patientRepo.SearchFor(u => u.UserId == userId).FirstOrDefault().PatientId;
        }

        public Patient GetPatientByUserId(string userId)
        {
            return this._patientRepo.SearchFor(u => u.UserId == userId).FirstOrDefault();
        }

        public int SavePatient(Patient patient)
        {
            this._patientRepo.Add(patient);
            return this._patientRepo.SaveChanges();
        }
    }
}

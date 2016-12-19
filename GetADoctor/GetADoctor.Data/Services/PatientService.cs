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
        List<Appointment> GetAppointmentsByPatientId(int Id);
        Appointment GetAppointmentById(int Id);
        bool IsAppointmentTaken(int userId, string date, int doctorId);
        bool IsAppointmentAvailable(int doctorId, String date);
        int RegisterAppointment(Appointment model);
    }
    public class PatientService : IPatientservice
    {
        private readonly IPatientRepository _patientRepo;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IScheduleRepository _scheduleRepo;

        public PatientService(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository,
            IScheduleRepository scheduleRepository)
        {
            this._patientRepo = patientRepository;
            this._appointmentRepo = appointmentRepository;
            this._scheduleRepo = scheduleRepository;
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

        public List<Appointment> GetAppointmentsByPatientId(int Id)
        {
            return this._appointmentRepo.SearchFor(p => p.PatientId == Id).ToList();
        }
        public int RegisterAppointment(Appointment model)
        {
            if(IsAppointmentAvailable(model.DoctorId, model.Date))
            {
                model.Serial = GenerateSerial(model.DoctorId, model.Date) + 1;
                this._appointmentRepo.Add(model);
                return this._appointmentRepo.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        public bool IsAppointmentTaken(int userId, string date, int doctorId)
        {
            var appointments = this._appointmentRepo.SearchFor(p => p.PatientId == userId).ToList();
            return appointments.Count(e => e.Date == date && e.DoctorId == doctorId) > 0;
        }
        public bool IsAppointmentAvailable(int doctorId, string date)
        {
            return _appointmentRepo.SearchFor(e => e.DoctorId == doctorId && e.Date == date).ToList().Count() < _scheduleRepo.SearchFor(e => e.DoctorId == doctorId && e.Dates == date).FirstOrDefault().PatientNumber;
            //return this._appointmentRepo.IsAppointmentAvailable(doctorId, date);
        }

        public int GenerateSerial(int doctorId, String date)
        {
            return this._appointmentRepo.SearchFor(e => e.DoctorId == doctorId && e.Date == date).ToList().Count();
        }

        public Appointment GetAppointmentById(int Id)
        {
            return this._appointmentRepo.Get(Id);
        }
    }
}

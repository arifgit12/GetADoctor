using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetDoctors();
        //List<Doctor> GetDoctors(int page, int size);
        //List<Doctor> SearchDoctors(String area, String speciality, int page, int size);
        Doctor GetDoctorByUserId(string userId);
        Doctor GetDoctor(int id);
        int GetDoctorId(string userId);
        int SaveDoctor(Doctor doctor);
        int UpdateDoctor(Doctor doctor);

        Location GetAddressByUserId(string userId);
        Location GetAddress(int id);
        int SaveAddress(Location address);
        int UpdateAddress(Location address);

        // Get All States
        IEnumerable<State> GetStates();
        State GetState(int id);

        IEnumerable<Schedule> GetAllScheduleByUserId(string userId);
        IEnumerable<Schedule> GetSchedulesByDoctorId(int id);
        //Schedule GetScheduleByUserId(String userId);
        Schedule GetScheduleById(int id);
        //Schedule GetSchedulesByDoctorId(int id);
        int SaveSchedule(Schedule model);
        int UpdateSchedule(Schedule model);

        IEnumerable<Speciality> GetSpecialities();
        IEnumerable<Appointment> GetAppointmentsByDoctorId(int Id);
    }
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILocationRepository _addressRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        //private readonly IWaitingRepository _waitingRepository;
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IStateRepository _stateRepository;

        public DoctorService(IDoctorRepository doctorRepository, ISpecialityRepository specialityRepository, 
            ILocationRepository addressRepository, IStateRepository stateRepository, IScheduleRepository scheduleRepository, IAppointmentRepository appointmentRepository)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _addressRepository = addressRepository;
            _stateRepository = stateRepository;
            _scheduleRepository = scheduleRepository;
            _appointmentRepository = appointmentRepository;
        }

        public Doctor GetDoctor(int id)
        {
            return this._doctorRepository.Get(id);
        }

        public int GetDoctorId(string userId)
        {
            return this._doctorRepository.SearchFor(d => d.UserId == userId).FirstOrDefault().DoctorId;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return this._doctorRepository.GetAll();
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

        public IEnumerable<Speciality> GetSpecialities()
        {
            return this._specialityRepository.GetAll();
        }

        public Location GetAddressByUserId(string userId)
        {
            return this._addressRepository.SearchFor(s => s.UserId == userId).FirstOrDefault();
        }

        public Location GetAddress(int id)
        {
            return this._addressRepository.Get(id);
        }

        public int SaveAddress(Location address)
        {
            this._addressRepository.Add(address);
            return this._addressRepository.SaveChanges();
        }

        public int UpdateAddress(Location address)
        {
            this._addressRepository.Update(address);
            return this._addressRepository.SaveChanges();
        }

        public IEnumerable<State> GetStates()
        {
            return this._stateRepository.GetAll();
        }

        public State GetState(int id)
        {
            return this._stateRepository.Get(id);
        }

        public IEnumerable<Schedule> GetAllScheduleByUserId(string userId)
        {
            var doctorId = _doctorRepository.SearchFor(d => d.UserId == userId).FirstOrDefault().DoctorId;
            var schedules = _scheduleRepository.SearchFor(d => d.DoctorId == doctorId).ToList();
            return schedules;
        }

        public Schedule GetScheduleById(int id)
        {
            return this._scheduleRepository.Get(id);
        }

        public IEnumerable<Schedule> GetSchedulesByDoctorId(int id)
        {
            return this._scheduleRepository.SearchFor(d => d.DoctorId == id);
        }

        public int SaveSchedule(Schedule model)
        {
            this._scheduleRepository.Add(model);
            return this._scheduleRepository.SaveChanges();
        }

        public int UpdateSchedule(Schedule model)
        {
            this._scheduleRepository.Update(model);
            return this._scheduleRepository.SaveChanges();
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctorId(int Id)
        {
            return this._appointmentRepository.SearchFor(d => d.DoctorId == Id).ToList();
        }
    }
}

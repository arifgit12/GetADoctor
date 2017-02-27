using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyString;
using GetADoctor.Models.Utilities;

namespace GetADoctor.Data.Services
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetDoctors();
        //List<Doctor> GetDoctors(int page, int size);
        List<Doctor> SearchDoctors(int? city = null, int? speciality = null);
        List<Doctor> SearchDoctors(string name = null, int? speciality = null, string search = null);
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
        Speciality GetSpecialtiesByDoctorId(int Id);
        IEnumerable<Appointment> GetAppointmentsByDoctorId(int Id);
        IEnumerable<Doctor> SearchSimilarDoctors(Doctor doctor);
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

        public List<Doctor> SearchDoctors(int? city = default(int?), int? speciality = default(int?))
        {

            var doctors = this._doctorRepository.SearchFor( u => (u.User.locations.Any( c => c.CityId == city | city == null )) && (u.SpecialityId == speciality | speciality == null) )
                .OrderBy(d => d.LastName)
                .ToList();

            // Get all users
            // Get all city of the users
            // Get all the doctors
            // Get doctors with speciality

            return doctors;
        }

        public List<Doctor> SearchDoctors(string name = null, int? speciality = default(int?), string search = null)
        {
            var doctors = this._doctorRepository.SearchFor(u => 
                    ( u.FirstName.ToLower().Contains(name.ToLower()) | u.LastName.ToLower().Contains(name.ToLower() ) | string.IsNullOrEmpty(name) ) &&
                    (u.User.locations.Any(c => c.City.CityName.ToLower().Contains(search.ToLower()) | string.IsNullOrEmpty(search)) ) &&
                    (u.SpecialityId == speciality | speciality == null))
               .OrderBy(d => d.LastName)
               .ToList();

            return doctors;
        }

        public IEnumerable<Doctor> SearchSimilarDoctors(Doctor doctor)
        {
            var resultDoctors = new List<Doctor>();
            var tempDoctors = new List<Doctor>();

            var doctors = this._doctorRepository.GetAll().ToList();

            foreach (var singledoctor in doctors)
            {
                var specialty = GetSpecialtiesByDoctorId(singledoctor.DoctorId);
                singledoctor.Speciality = specialty;
            }

            //check if there is a exactly match, if so, top 1
            if (doctors.Contains(doctor))
            {
                resultDoctors.Add(doctors.Find(x => x.Equals(doctor)));//100% match
                doctors.Remove(doctor); //remove it
            }

            //check Name matchs use Dice Distance, if pass name
            //rule if result over 0.5, means similar
            if (!string.IsNullOrEmpty(doctor.FirstName))
            {
                tempDoctors.AddRange(doctors.Where(singledoctor => doctor.FirstName.SorensenDiceDistance(singledoctor.FirstName) < 0.5));
            }

            resultDoctors.AddRange(tempDoctors);
            tempDoctors.Clear();

            /*Check Specialties
            * 2 possible , 
            * 1. resultDoctors is not empty, get the doctors who contain the Specialties
            * 2, resultDoctors is empty, get all doctors who contain the Specialties
            * If Specialties more than one, consider individual
            */

            if (doctor.Speciality != null)
            {
                // This is required when doctors have more than one speciality
                //foreach (var specialty in doctor.Specialties)
                //{
                //    tempDoctors.AddRange(resultDoctors.Count != 0
                //        ? resultDoctors.Where(t => t.Specialties.Contains(specialty)).ToList()
                //        : doctors.Where(t => t.Specialties.Contains(specialty)).ToList());
                //}
            }

            //remove duplicate doctors
            resultDoctors = tempDoctors.Count != 0 ? tempDoctors.Distinct(new DoctorComparer()).ToList() : tempDoctors;

            return resultDoctors;
        }

        public Speciality GetSpecialtiesByDoctorId(int Id)
        {
            var speciality = this._doctorRepository.SearchFor(d => d.DoctorId == Id).FirstOrDefault().Speciality;
            return speciality;
        }
    }
}

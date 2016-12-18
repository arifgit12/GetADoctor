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

        //Location GetAddressByUserId(string userId);
        //Location GetAddress(int id);
        //int SaveAddress(Location address, string userId);
        //int UpdateAddress(Location address, string userId);
        //object GetDoctorUserId(Task<string> task);

        //IEnumerable<Schedule> GetAllScheduleByUserId(string userId);
        //Schedule GetScheduleByUserId(String userId);
        //Schedule GetScheduleById(int id);
        //Schedule GetSchedulesByDoctorId(int id);
        //int SaveSchedule(Schedule model, int DoctorId);
        //int UpdateSchedule(Schedule model, int DoctorId);

        IEnumerable<Speciality> GetSpecialities();
    }
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        //private readonly ILocationRepository _addressRepository;
        //private readonly IScheduleRepository _scheduleRepository;
        //private readonly IAppointmentRepository _appointmentRepository;
        //private readonly IWaitingRepository _waitingRepository;
        private readonly ISpecialityRepository _specialityRepository;

        public DoctorService(IDoctorRepository doctorRepository, ISpecialityRepository specialityRepository)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
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
    }
}

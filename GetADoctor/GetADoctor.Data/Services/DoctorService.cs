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
        List<Doctor> GetDoctors(int page, int size);
        List<Doctor> SearchDoctors(String area, String speciality, int page, int size);

        Doctor GetDoctorByUserName(string username);
        Doctor GetDoctorUserId(string userId);
        Doctor GetDoctor(int id);
        int GetDoctorId(string userId);
        int SaveDoctor(Doctor doctor);
        int SaveProfile(Doctor doctor, string userId);
        int EditProfile(Doctor doctor, string userId);

        Location GetAddressByUserId(string userId);
        Location GetAddress(int id);
        int SaveAddress(Location address, string userId);
        int UpdateAddress(Location address, string userId);
        object GetDoctorUserId(Task<string> task);

        //IEnumerable<Schedule> GetAllScheduleByUserId(string userId);
        //Schedule GetScheduleByUserId(String userId);
        //Schedule GetScheduleById(int id);
        //Schedule GetSchedulesByDoctorId(int id);
        //int SaveSchedule(Schedule model, int DoctorId);
        //int UpdateSchedule(Schedule model, int DoctorId);
    }
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILocationRepository _addressRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IWaitingRepository _waitingRepository;

        public DoctorService(IDoctorRepository doctorRepository, ILocationRepository addressRepository, IScheduleRepository scheduleRepository, IAppointmentRepository appointmentRepository, IWaitingRepository waitingRepository)
        {
            _doctorRepository = doctorRepository;
            _addressRepository = addressRepository;
            _scheduleRepository = scheduleRepository;
            _appointmentRepository = appointmentRepository;
            _waitingRepository = waitingRepository;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return this._doctorRepository.GetAll();
        }

        public List<Doctor> GetDoctors(int page, int size)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> SearchDoctors(string area, string speciality, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Doctor GetDoctorByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public Doctor GetDoctorUserId(string userId)
        {
            return this._doctorRepository.GetByUser(userId);
        }

        public Doctor GetDoctor(int id)
        {
            return this._doctorRepository.Get(id);
        }

        public int GetDoctorId(string userId)
        {
            throw new NotImplementedException();
        }

        public int SaveDoctor(Doctor doctor)
        {
            this._doctorRepository.Add(doctor);
            return this._doctorRepository.SaveChanges();
        }

        public int SaveProfile(Doctor doctor, string userId)
        {
            throw new NotImplementedException();
        }

        public int EditProfile(Doctor doctor, string userId)
        {
            throw new NotImplementedException();
        }

        public Location GetAddressByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Location GetAddress(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveAddress(Location address, string userId)
        {
            throw new NotImplementedException();
        }

        public int UpdateAddress(Location address, string userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Doctor> IDoctorService.GetDoctors()
        {
            throw new NotImplementedException();
        }

        List<Doctor> IDoctorService.GetDoctors(int page, int size)
        {
            throw new NotImplementedException();
        }

        List<Doctor> IDoctorService.SearchDoctors(string area, string speciality, int page, int size)
        {
            throw new NotImplementedException();
        }

        Doctor IDoctorService.GetDoctorByUserName(string username)
        {
            throw new NotImplementedException();
        }

        Doctor IDoctorService.GetDoctorUserId(string userId)
        {
            throw new NotImplementedException();
        }

        Doctor IDoctorService.GetDoctor(int id)
        {
            throw new NotImplementedException();
        }

        int IDoctorService.GetDoctorId(string userId)
        {
            throw new NotImplementedException();
        }

        int IDoctorService.SaveDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        int IDoctorService.SaveProfile(Doctor doctor, string userId)
        {
            throw new NotImplementedException();
        }

        int IDoctorService.EditProfile(Doctor doctor, string userId)
        {
            throw new NotImplementedException();
        }

        Location IDoctorService.GetAddressByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        Location IDoctorService.GetAddress(int id)
        {
            throw new NotImplementedException();
        }

        int IDoctorService.SaveAddress(Location address, string userId)
        {
            throw new NotImplementedException();
        }

        int IDoctorService.UpdateAddress(Location address, string userId)
        {
            throw new NotImplementedException();
        }

        object IDoctorService.GetDoctorUserId(Task<string> task)
        {
            throw new NotImplementedException();
        }
    }
}

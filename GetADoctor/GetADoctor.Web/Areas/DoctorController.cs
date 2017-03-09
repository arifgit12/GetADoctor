using AutoMapper;
using GetADoctor.Data.Services;
using GetADoctor.Models;
using GetADoctor.Web.Models;
using GetADoctor.Web.Models.Doctors;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : BaseController
    {
        private readonly IDoctorService _doctorService;
        private readonly ICityService cityService;
        private const int ItemsPerPage = 10;
        public DoctorController(ApplicationUserManager userManager, IDoctorService doctorservice, ICityService cityService)
            : base(userManager)
        {
            this._doctorService = doctorservice;
            this.cityService = cityService;
        }
        
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor/Appointments
        public async Task<ActionResult> Appointments()
        {
            string userId = await GetUserId();
            var doctorId = _doctorService.GetDoctorId(userId);
            var appointments = _doctorService.GetAppointmentsByDoctorId(doctorId);
            var model = Mapper.Map<IEnumerable<AppointmentViewModel>>(appointments);
            return View(model);
        }

        public async Task<ActionResult> Schedules()
        {
            string userId = await GetUserId();
            var schedules = _doctorService.GetAllScheduleByUserId(userId);
            var model = Mapper.Map<IEnumerable<ScheduleViewModel>>(schedules);
            return View(model);
        }
        public ActionResult Schedule()
        {
            var model = new ScheduleViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Schedule(ScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = await GetUserId();
                var doctorId = _doctorService.GetDoctorId(userId);
                var schedule = Mapper.Map<Schedule>(model);
                schedule.DoctorId = doctorId;
                schedule.CreatedOn = DateTime.UtcNow;
                schedule.UpdatedOn = DateTime.UtcNow;

                var isSave = _doctorService.SaveSchedule(schedule);
                if (isSave > 0)
                {
                    return RedirectToAction("Schedules");
                }
            }

            return View(model);
        }

        public ActionResult EditSchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schedule schedule = _doctorService.GetScheduleById(id.Value);
            if(schedule == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<ScheduleViewModel>(schedule);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchedule(ScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var schedule = _doctorService.GetScheduleById(model.Id);
                // Can put validation if schedule is found or not
                schedule = Mapper.Map(model, schedule);
                var isUpdate = _doctorService.UpdateSchedule(schedule);
                if (isUpdate > 0)
                {
                    return RedirectToAction("Schedules");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult All(int page = 1, int? city = null, int? speciality = null)
        {

            ViewBag.CityId = city;
            ViewBag.SpecialityId = speciality;

            var docmodel = this._doctorService
                .SearchDoctors(city, speciality);

            var doctors = AutoMapper.Mapper.Map<IEnumerable<DoctorViewModel>>(docmodel);

            var citiesList = this.cityService
                .GetCities()
                .Select(c => new SelectListItem { Text = c.CityName, Value = c.CityId.ToString() })
                .ToList();
            var selectedAll = new SelectListItem { Text = "All", Value = "" };
            citiesList.Insert(0, selectedAll);

            var specialitiesList = this._doctorService
                .GetSpecialities()
                .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() })
                .ToList();
            specialitiesList.Insert(0, selectedAll);

            var model = new FilterDoctorsViewModel()
            {
                Doctors = new PagedList<DoctorViewModel>(doctors, page, ItemsPerPage),
                Cities = citiesList,
                Specialities = specialitiesList
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get(int? id)
        {
            if(id == null)
            {
                return this.HttpNotFound("No such doctor existing.");
            }

            var doctor = this._doctorService
                 .GetDoctor(id.Value);

            if (doctor == null)
            {
                return this.HttpNotFound("No such doctor existing.");
            }

            var existingDoctor = Mapper.Map<DoctorViewModel>(doctor);
            var schedules  = _doctorService.GetSchedulesByDoctorId(doctor.DoctorId).ToList();
            ViewBag.Schedules = schedules.Select(s => new SelectListItem
            {
                Value = s.Dates.ToString(),
                Text = s.Dates
            });

            ViewBag.UserName = doctor.User.UserName;
            ViewBag.FileName = doctor.User.ProfilePicUrl;
            return View(existingDoctor);
        }
    }
}
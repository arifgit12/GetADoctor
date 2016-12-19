using AutoMapper;
using GetADoctor.Data.Services;
using GetADoctor.Models;
using GetADoctor.Web.Models;
using System;
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

        public DoctorController(ApplicationUserManager userManager, IDoctorService doctorservice)
            : base(userManager)
        {
            this._doctorService = doctorservice;
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
    }
}
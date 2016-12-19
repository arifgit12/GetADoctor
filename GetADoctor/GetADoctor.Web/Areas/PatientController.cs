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
    public class PatientController : BaseController
    {
        private IDoctorService _doctorService;
        private IPatientservice _patientService;

        public PatientController(ApplicationUserManager userManager,
            IDoctorService doctorService, IPatientservice patientService)
            : base(userManager)
        {
            this._doctorService = doctorService;
            this._patientService = patientService;
        }

        // GET: Patient
        public ActionResult Index(int page = 0)
        {
            int size = 5;
            var doctors = _doctorService.GetDoctors().Skip(page * size).Take(size).ToList();
            var model = Mapper.Map<IEnumerable<HomeDoctorViewModel>>(doctors);
            return View(model);
        }

        // GET: Patient/Appointments
        public async Task<ActionResult> Appointments()
        {
            // Get PatientId
            var userId = await GetUserId();
            var patientId = this._patientService.GetPatientId(userId);
            var appointments = this._patientService.GetAppointmentsByPatientId(patientId);
            var model = Mapper.Map<IEnumerable<AppointmentViewModel>>(appointments);
            return View(model);
        }

        // GET: Patient/Appointment
        public ActionResult Appointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = _doctorService.GetDoctor(id.Value);
            if(doctor == null)
            {
                return HttpNotFound();
            }
            var model = new AppointmentViewModel();
            model.DoctorId = doctor.DoctorId;
            model.Doctor = doctor;
            var schedules = _doctorService.GetSchedulesByDoctorId(doctor.DoctorId).ToList();
            model.ScheduleDates = schedules.Select(s => new SelectListItem
            {
                Value = s.Dates.ToString(),
                Text = s.Dates
            });
            return View(model);
        }

        // POST: Patient/Appointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Appointment(AppointmentViewModel model)
        {
            // Get PatientId
            var userId = await GetUserId();
            var patientId = this._patientService.GetPatientId(userId);
            var schedules = _doctorService.GetSchedulesByDoctorId(model.DoctorId);
            model.ScheduleDates = schedules.ToList().Select(s => new SelectListItem
            {
                Value = s.Dates.ToString(),
                Text = s.Dates
            });
            if (ModelState.IsValid)
            {
                model.PatientId = patientId;
                model.CreatedOn = DateTime.UtcNow;
                model.UpdatedOn = DateTime.UtcNow;
                model.Uid = Guid.NewGuid().ToString();
                var appointment = Mapper.Map<Appointment>(model);

                if (_patientService.IsAppointmentTaken(appointment.PatientId, appointment.Date, appointment.DoctorId))
                {
                    //return Json("Appointment already taken", JsonRequestBehavior.AllowGet);
                    model.Doctor = _doctorService.GetDoctor(model.DoctorId);
                    TempData["UnSuccess"] = " Appointment Already Taken";
                    return View(model);
                }

                var isSave = _patientService.RegisterAppointment(appointment);
                if(isSave > 0)
                {
                    return RedirectToAction("Appointments");
                }
                else
                {
                    TempData["UnSuccess"] = " Appointment Not Avaiable";
                }

            }
            model.Doctor = _doctorService.GetDoctor(model.DoctorId);
            return View(model);
        }

        // GET: Patient/CancelAppointment/5
        public ActionResult CancelAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appointment = _patientService.GetAppointmentById(id.Value);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<AppointmentViewModel>(appointment);
            return View(model);
        }

        // POST: Patient/CancelAppointment/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            // Cancel the appointment
            var appointment = _patientService.GetAppointmentById(id);
            return RedirectToAction("Appointments");
        }
    }
}
using GetADoctor.Data.Services;
using GetADoctor.Models;
using GetADoctor.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
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

        // GET: Doctor/EditProfile
        public async Task<ActionResult> EditProfile()
        {
            var doc = _doctorService.GetDoctors();
            string userId = await GetUserId();
            var doctor = _doctorService.GetDoctorByUserId(userId);
            var model = AutoMapper.Mapper.Map<DoctorViewModel>(doctor);
            ViewBag.SpecialityId = new SelectList(this._doctorService.GetSpecialities(), "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = await GetUserId();
                var doctordb = _doctorService.GetDoctorByUserId(userId);
                var doctor = AutoMapper.Mapper.Map<Doctor>(model);
                doctor.UserId = doctordb.UserId;
                
                var isUpdated = _doctorService.UpdateDoctor(doctor);
                if(isUpdated > 0)
                {
                    return RedirectToAction("Index", "Doctor");
                }

            }

            return View(model);
        }

        [NonAction]
        public virtual async System.Threading.Tasks.Task<string> GetUserId()
        {
            string userName = this.User.Identity.Name;
            var user = await UserManager.FindByNameAsync(userName);
            return user.Id;
        }

    }
}
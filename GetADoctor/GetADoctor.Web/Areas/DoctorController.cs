using GetADoctor.Data.Services;
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
        private IDoctorService _doctorService;
        public DoctorController(ApplicationUserManager userManager, IDoctorService doctorservice) 
            : base(userManager)
        {
            DoctorService = doctorservice;
        }

        public IDoctorService DoctorService
        {
            get
            {
                return _doctorService;
            }
            private set
            {
                _doctorService = value;
            }
        }

        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor/EditProfile
        public ActionResult EditProfile()
        {
            var doctor = _doctorService.GetDoctorUserId(GetUserId());

            //var doctor = _doctorService.GetDoctor(User.Identity.GetUserId);
            return View();
        }

    }
}
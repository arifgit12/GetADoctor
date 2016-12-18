using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        // GET: Patient/Appointments
        public ActionResult Appointments()
        {            
            return View();
        }

        // GET: Patient/Appointment
        public ActionResult Appointment()
        {           
            return View();
        }
    }
}
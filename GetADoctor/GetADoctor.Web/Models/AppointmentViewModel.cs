using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }

        [Display(Name = "Taken At")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Updated At")]
        public DateTime UpdatedOn { get; set; }

        [Display(Name = "Visiting Time")]
        public String VisitingTime { get; set; }

        [Display(Name = "Booking Date")]
        public String Date { get; set; }

        [Display(Name = "Serial")]
        public int Serial { get; set; }

        [Display(Name = "UID")]
        public String Uid { get; set; }

        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public IEnumerable<SelectListItem> ScheduleDates { get; set; }
    }
}
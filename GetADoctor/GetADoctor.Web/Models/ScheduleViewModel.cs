using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Dates")]
        public String Dates { get; set; }

        [Display(Name = "From Hour")]
        public int FromHour { get; set; }

        [Display(Name = "From Minute")]
        public int FromMinute { get; set; }

        [Display(Name = "To Hour")]
        public int ToHour { get; set; }

        [Display(Name = "To Minute")]
        public int ToMinute { get; set; }

        [Display(Name = "Total Patient")]
        public int PatientNumber { get; set; }

        [Display(Name = "Visiting Fee")]
        public int VisitingFee { get; set; }

        [Display(Name = "Registration Fee")]
        public int RegistrationFee { get; set; }

        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
    }
}
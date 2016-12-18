using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }

        [Required]
        public String Name { get; set; }
        public int Age { get; set; }
        public String Gender { get; set; }
        public String MobileNumber { get; set; }
    }
}
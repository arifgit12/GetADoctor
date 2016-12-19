using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class HomeDoctorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public City City { get; set; }
        public Speciality Speciality { get; set; }
    }
}
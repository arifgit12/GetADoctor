using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models.Specialities
{
    public class SpecialityDoctorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CityName { get; set; }
        public float Rating { get; set; }
        public int CommentsCount { get; set; }
    }
}
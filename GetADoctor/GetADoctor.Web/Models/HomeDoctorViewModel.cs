using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class HomeDoctorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public City City { get; set; }
        public Speciality Speciality { get; set; }
        [Display(Name = "Ratings")]
        public float? Rating { get; set; }
        [Display(Name = "Ratings Score")]
        public int? RatingsCount { get; set; }
        [Display(Name = "Comments Score")]
        public int? CommentsCount { get; set; }
    }
}
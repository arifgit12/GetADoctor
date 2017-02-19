using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [Display(Name = "Unique Identification Number")]
        public string UIN { get; set; }
       
        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public String Gender { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Entered phone number is not valid.")]
        public String MobileNumber { get; set; }

        public String ImageUrl { get; set; }

        [Display(Name = "Specialization")]
        public int? SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }

        public String HouseNo { get; set; }
        public String RoadNo { get; set; }

        
        public String CityName { get; set; }
        public String StateName { get; set; }
    }
}
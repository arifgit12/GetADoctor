using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Areas.Admin.Models
{
    public class SpecialityViewModel : SystemEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Code")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Please enter 4 digits number only")]
        public int Code { get; set; }

        [Required]
        [MaxLength(80)]
        [Display(Name = "Specilization Name")]
        public string Name { get; set; }
    }
}
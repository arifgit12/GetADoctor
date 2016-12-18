using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetADoctor.Web.Areas.Admin.Models
{
    public class StateViewModel : SystemEntity
    {        
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        public List<City> Cities = new List<City>();
    }
}
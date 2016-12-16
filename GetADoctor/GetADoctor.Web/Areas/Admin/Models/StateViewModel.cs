using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetADoctor.Web.Areas.Admin.Models
{
    public class StateViewModel : SystemEntity
    {
        [Required]
        [MaxLength(40)]
        public string StateName { get; set; }

        private ICollection<City> cities { get; set; }
    }
}
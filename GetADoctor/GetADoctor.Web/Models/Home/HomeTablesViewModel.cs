using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Models.Home
{
    public class HomeTablesViewModel
    {
        [Display(Name = "Most Commented")]
        public IEnumerable<HomeDoctorViewModel> MostCommented { get; set; }

        [Display(Name = "Highest Rating")]
        public IEnumerable<HomeDoctorViewModel> HighestRating { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Specialities { get; set; }
    }
}
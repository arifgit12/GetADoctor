using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models.Home
{
    public class HomeTablesViewModel
    {
        [Display(Name = "Most Commented")]
        public IEnumerable<HomeDoctorViewModel> MostCommented { get; set; }

        [Display(Name = "Highest Rating")]
        public IEnumerable<HomeDoctorViewModel> HighestRating { get; set; }
    }
}
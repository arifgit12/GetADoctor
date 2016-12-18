using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GetADoctor.Web.Models
{
    public class CityListViewModel
    {
        public List<City> CityList = new List<City>();

        public int CityId { get; set; }
        public IEnumerable<SelectListItem> CityIEnum
        {
            get
            {
                return new SelectList(CityList, "CityId", "CityName");
            }
        }
    }
}
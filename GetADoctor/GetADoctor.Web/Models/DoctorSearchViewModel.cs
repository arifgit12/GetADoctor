using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class DoctorSearchViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Uin { get; set; }

        public string CityName { get; set; }
    }
}
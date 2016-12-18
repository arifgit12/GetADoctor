using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Models
{
    public class AddressViewModel : SystemEntity
    {
        public int LocationId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        public string Zipcode { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
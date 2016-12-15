using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        public string Zipcode { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

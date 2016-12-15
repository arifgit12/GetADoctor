using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class City : SystemEntity
    {
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(40)]
        public string CityName { get; set; }

        public int StateId { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}

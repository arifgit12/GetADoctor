using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class State : SystemEntity
    {
        public State()
        {
            this.cities = new HashSet<City>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(40)]
        public string StateName { get; set; }

        private ICollection<City> cities;
        public virtual ICollection<City> Cities
        {
            get { return this.cities; }
            set { this.cities = value; }
        }
    }
}

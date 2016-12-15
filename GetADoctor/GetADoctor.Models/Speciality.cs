using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class Speciality : SystemEntity
    {
        private ICollection<Doctor> doctors;

        public Speciality()
        {
            this.doctors = new HashSet<Doctor>();
        }
        public int Code { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(80)]
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors
        {
            get { return this.doctors; }
            set { this.doctors = value; }
        }
    }
}

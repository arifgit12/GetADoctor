using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public String Gender { get; set; }
        public String MobileNumber { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class Schedule : SystemEntity
    {
        public String Dates { get; set; }
        public int FromHour { get; set; }
        public int FromMinute { get; set; }
        public int ToHour { get; set; }
        public int ToMinute { get; set; }
        public int PatientNumber { get; set; }
        public int VisitingFee { get; set; }
        public int RegistrationFee { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetADoctor.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.comments = new HashSet<Comment>();
            this.rating = new HashSet<Rating>();
        }

        [Key]
        public int DoctorId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(10)]

        [Display(Name = "Unique Identification Number")]
        public string UIN { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public String Gender { get; set; }
        public String MobileNumber { get; set; }
        public String ImageUrl { get; set; }

        [ForeignKey("Speciality")]
        public int? SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        private ICollection<Comment> comments;
        private ICollection<Rating> rating;

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Rating> Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }

        public string DisplayName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetADoctor.Models
{
    public enum GENDER
    {
        MALE,
        FEMALE
    }
    public enum UserRoles
    {
        Administrator = 1,
        Doctor = 2,
        Patient = 4,
        User = 8,
    }
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Doctor> doctors;
        private ICollection<Comment> comments;
        private ICollection<Rating> ratings;
        private ICollection<Patient> patients;

        public ApplicationUser()
        {
            this.doctors = new HashSet<Doctor>();
            this.comments = new HashSet<Comment>();
            this.ratings = new HashSet<Rating>();
            this.patients = new HashSet<Patient>();
        }

        public ICollection<Doctor> Doctors
        {
            get { return this.doctors; }
            set { this.doctors = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<Patient> Patients
        {
            get { return this.patients; }
            set { this.patients = value; }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface IUserService
    {
        ApplicationUser GetUser(string userId);
        IEnumerable<ApplicationUser> GetUsers();
        IEnumerable<ApplicationUser> GetUsers(string username);
        ApplicationUser GetUserProfile(string userid);
        ApplicationUser GetUsersByEmail(string email);
        IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid);
        IEnumerable<ApplicationUser> SearchUser(string searchString);

        IEnumerable<ValidationResult> CanAddUser(string email);
        void UpdateUser(ApplicationUser user);
        void SaveUser();
        void SaveImageURL(string userId, string imageUrl);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IEnumerable<ValidationResult> CanAddUser(string email)
        {
            var user = userRepository.SearchFor(u => u.Email == email);
            if (user != null)
            {
                yield return new ValidationResult("Email", new[] { "Email already exits!" });
            }
        }

        public ApplicationUser GetUser(string userId)
        {
            return userRepository.SearchFor(u => u.Id == userId).FirstOrDefault();
        }

        public IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserProfile(string userid)
        {
            var userprofile = userRepository.SearchFor(u => u.Id == userid).SingleOrDefault();
            return userprofile;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            var users = userRepository.GetAll();
            return users;
        }

        public IEnumerable<ApplicationUser> GetUsers(string username)
        {
            var users = userRepository.SearchFor(u => (u.UserName).Contains(username) || u.Email.Contains(username)).OrderBy(u => u.UserName).ToList();

            return users;
        }

        public ApplicationUser GetUsersByEmail(string email)
        {
            var users = userRepository.SearchFor(u => u.Email.Contains(email)).SingleOrDefault();
            return users;
        }

        public void SaveImageURL(string userId, string imageUrl)
        {
            var user = GetUser(userId);
            user.ProfilePicUrl = imageUrl;
            UpdateUser(user);
        }

        public void SaveUser()
        {
            this.userRepository.SaveChanges();
        }

        public IEnumerable<ApplicationUser> SearchUser(string searchString)
        {
            var users = userRepository.SearchFor(u => u.UserName.Contains(searchString) || u.Email.Contains(searchString)).OrderBy(u => u.UserName);
            return users;
        }

        public void UpdateUser(ApplicationUser user)
        {
            userRepository.Update(user);
            SaveUser();
        }
    }
}

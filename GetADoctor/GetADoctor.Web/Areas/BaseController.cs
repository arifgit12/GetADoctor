using GetADoctor.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;

        public BaseController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region Helpers
        [NonAction]
        public void AddErrorString(string error)
        {
            ModelState.AddModelError("", error);
        }


        [NonAction]
        public virtual string GetUserName()
        {
            string userId = this.User.Identity.Name;
            return userId;
        }

        [NonAction]
        public virtual async System.Threading.Tasks.Task<string> GetUserId()
        {
            string userName = this.User.Identity.Name;
            var user = await UserManager.FindByNameAsync(userName);
            return user.Id;
        }

        [NonAction]
        public virtual async System.Threading.Tasks.Task<ApplicationUser> GetUser()
        {
            string username = this.User.Identity.Name;
            var user = await UserManager.FindByNameAsync(username);
            return user;
        }
        #endregion
    }
}
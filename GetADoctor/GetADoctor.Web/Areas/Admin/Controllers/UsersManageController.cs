using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersManageController : Controller
    {
        private ApplicationUserManager _userManager;

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
        // GET: Admin/UsersManage
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            return View(users);
        }
    }
}
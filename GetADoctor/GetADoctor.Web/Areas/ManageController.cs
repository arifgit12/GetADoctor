using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GetADoctor.Web.Models;
using GetADoctor.Data.Services;
using GetADoctor.Models;
using AutoMapper;
using GetADoctor.Web.Infrastructure.FileHelper;

namespace GetADoctor.Web.Areas
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ILocationservice _locationService;
        private IProfileService _profileService;

        //public ManageController()
        //{
        //}

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            ILocationservice locationService, IProfileService profileService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._locationService = locationService;
            this._profileService = profileService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.AddressAddSuccess ? "Your Address Added Successfully"
                : message == ManageMessageId.AddressUpdateSuccess ? "Your Address Updated Successfully"
                : "";

            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                HasAddress = _locationService.GetAddressByUserId(userId) != null ? true : false
            };
            ViewBag.UserName = User.Identity.GetUserName();
            ViewBag.FileName = user.ProfilePicUrl;
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/ProfilePicture
        public ActionResult ProfilePicture()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProfilePicture(HttpPostedFileBase upload)
        {
            if (upload.ContentLength > 0)
            {
                string uploadedFileName = FileUtils.UploadFile(upload, User.Identity.GetUserName());
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.ProfilePicUrl = uploadedFileName;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        /**********************DOCTOR PROFILE***********************************************************/

        // GET: Manage/DoctorProfile
        public async Task<ActionResult> DoctorProfile()
        {
            string userId = await GetUserId();
            var doctor = _profileService.GetDoctorByUserId(userId);
            var model = AutoMapper.Mapper.Map<DoctorViewModel>(doctor);

            if(model.SpecialityId > 0 )
            {
                ViewBag.SpecialityId = new SelectList(this._profileService.GetSpecialities(), "Id", "Name", model.SpecialityId);
            }
            else
            {
                ViewBag.SpecialityId = new SelectList(this._profileService.GetSpecialities(), "Id", "Name");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DoctorProfile(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = await GetUserId();
                var doctordb = _profileService.GetDoctorByUserId(userId);
                Mapper.Map(model, doctordb);

                var isUpdated = _profileService.UpdateDoctor(doctordb);
                if (isUpdated > 0)
                {
                    return RedirectToAction("Index", "Manage");
                }
            }
            if (model.SpecialityId == null)
            {
                ViewBag.SpecialityId = new SelectList(this._profileService.GetSpecialities(), "Id", "Name");
            }
            else
            {
                ViewBag.SpecialityId = new SelectList(this._profileService.GetSpecialities(), "Id", "Name", model.SpecialityId);
            }

            return View(model);
        }

        /**************************PATIENT PROFILE********************************************/

        // GET: Manage/UserProfile
        public async Task<ActionResult> UserProfile()
        {
            string userId = await GetUserId();
            var patient = _profileService.GetPatientByUserId(userId);
            var model = Mapper.Map<PatientViewModel>(patient);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserProfile(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = await GetUserId();
                var patient = _profileService.GetPatientByUserId(userId);
                Mapper.Map(model, patient);

                var isUpdated = _profileService.UpdatePatient(patient);
                if (isUpdated > 0)
                {
                    return RedirectToAction("Index", "Manage");
                }
            }
            return View(model);
        }

        /**************************BEGIN ADDRESS**********************************************/

        [HttpGet]
        public async Task<ActionResult> AddAddress()
        {
            var userId = await GetUserId();
            var address = _locationService.GetAddressByUserId(userId);
            if (address != null)
            {
                TempData["Error"] = "Address Already Present";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAddress(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetUserId();
                if (_locationService.GetAddressByUserId(userId) == null)
                {
                    var address = new Location();
                    Mapper.Map(model, address);

                    address.UserId = userId;
                    address.CreatedOn = DateTime.UtcNow;
                    address.UpdatedOn = DateTime.UtcNow;                    

                    var isSave = _locationService.SaveAddress(address);
                    if (isSave > 0)
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                }

            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditAddress()
        {
            var userId = await GetUserId();
            var address = _locationService.GetAddressByUserId(userId);
            if (address == null)
            {
                TempData["Error"] = "No Address Found";
                return View();
            }

            var model = Mapper.Map<AddressViewModel>(address);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAddress(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetUserId();
                var address = _locationService.GetAddressByUserId(userId);
                if (address == null)
                {
                    TempData["Error"] = "No Address Found";
                }
                else
                {
                    address = Mapper.Map(model, address);
                    address.UpdatedOn = DateTime.UtcNow;

                    var isSave = _locationService.UpdateAddress(address);
                    if (isSave > 0)
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                }

            }

            return View(model);
        }

        public static StateListViewModel slvm = new StateListViewModel();
        public ActionResult StateView()
        {
            slvm.StateList.Clear();
            var states = _locationService.GetStates();
            foreach (State state in states)
            {
                slvm.StateList.Add(state);
            }

            return View(slvm);
        }

        public static CityListViewModel clvm = new CityListViewModel();
        public ActionResult CityView(int? stateId)
        {
            clvm.CityList.Clear();
            if (stateId != null)
            {
                var states = _locationService.GetState(stateId.Value);

                foreach (City cpd in states.Cities)
                {
                    clvm.CityList.Add(cpd);
                }
            }
            return View(clvm);
        }

        /**************************END ADDRESS**********************************************/
        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        [NonAction]
        public async Task<string> GetUserId()
        {
            string userName = this.User.Identity.Name;
            var user = await UserManager.FindByNameAsync(userName);
            return user.Id;
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            AddressAddSuccess,
            AddressUpdateSuccess,
            Error
        }

#endregion
    }
}
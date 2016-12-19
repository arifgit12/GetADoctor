using AutoMapper;
using GetADoctor.Data.Services;
using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using GetADoctor.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : BaseController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(ApplicationUserManager userManager, IDoctorService doctorservice)
            : base(userManager)
        {
            this._doctorService = doctorservice;
        }
        

        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor/EditProfile
        public async Task<ActionResult> EditProfile()
        {
            var doc = _doctorService.GetDoctors();
            string userId = await GetUserId();
            var doctor = _doctorService.GetDoctorByUserId(userId);
            var model = AutoMapper.Mapper.Map<DoctorViewModel>(doctor);
            ViewBag.SpecialityId = new SelectList(this._doctorService.GetSpecialities(), "Id", "Name", doctor.SpecialityId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetUser();
                var doctordb = _doctorService.GetDoctorByUserId(user.Id);
                Mapper.Map(model, doctordb);
                
                var isUpdated = _doctorService.UpdateDoctor(doctordb);
                if(isUpdated > 0)
                {
                    return RedirectToAction("Index", "Doctor");
                }

            }

            return View(model);
        }

        public async Task<ActionResult> AddAddress()
        {
            var userId = await GetUserId();
            var address = _doctorService.GetAddressByUserId(userId);
            if(address != null)
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
                if(_doctorService.GetAddressByUserId(userId) == null)
                {
                    var address = new Location();
                    address.UserId = userId;
                    address.CreatedOn = DateTime.UtcNow;
                    address.UpdatedOn = DateTime.UtcNow;

                    Mapper.Map(model, address);

                    var isSave = _doctorService.SaveAddress(address);
                    if (isSave > 0)
                    {
                        return RedirectToAction("Index", "Doctor");
                    }
                }
                
            }
            
            return View(model);
        }

        public async Task<ActionResult> EditAddress()
        {
            var userId = await GetUserId();
            var address = _doctorService.GetAddressByUserId(userId);
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
                var address = _doctorService.GetAddressByUserId(userId);
                if (address == null)
                {
                    TempData["Error"] = "No Address Found";
                }
                else
                {
                    address = Mapper.Map(model, address);
                    address.UpdatedOn = DateTime.UtcNow;

                    var isSave = _doctorService.UpdateAddress(address);
                    if (isSave > 0)
                    {
                        return RedirectToAction("Index", "Doctor");
                    }
                }

            }

            return View(model);
        }

        public static StateListViewModel slvm = new StateListViewModel();
        public ActionResult StateView()
        {
            slvm.StateList.Clear();
            var states = _doctorService.GetStates();
            foreach(State state in states)
            {
                slvm.StateList.Add(state);
            }

            return View(slvm);
        }

        public static GetADoctor.Web.Models.CityListViewModel clvm = new CityListViewModel();
        public ActionResult CityView(int? stateId)
        {
            clvm.CityList.Clear();
            if (stateId != null)
            {
                var states = _doctorService.GetState(stateId.Value);

                foreach (City cpd in states.Cities)
                {
                    clvm.CityList.Add(cpd);
                }
            }
            return View(clvm);
        }
    }
}
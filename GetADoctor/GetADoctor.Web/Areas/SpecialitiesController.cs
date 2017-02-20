using GetADoctor.Data.Services;
using GetADoctor.Web.Models.Specialities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    [Authorize]
    public class SpecialitiesController : BaseController
    {
        private readonly ISpecialityService specialityService;
        private const int ItemPerPage = 15;

        public SpecialitiesController(ApplicationUserManager userManager, ISpecialityService specialityService) : base(userManager)
        {
            this.specialityService = specialityService;
        }

        // GET: Specialities
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult All(int page = 1)
        {
            var pagesCount = (int)Math.Ceiling(this.specialityService.GetSpecialities().Count() / (decimal)ItemPerPage);

            var specialities = AutoMapper.Mapper.Map<IEnumerable<HomeSpecialityViewModel>>(this.specialityService
                .GetSpecialities()
                .ToList());

            var model = new PagedList<HomeSpecialityViewModel>(specialities, page, ItemPerPage);

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get(int? id, int page = 1)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var existingSpeciality = AutoMapper.Mapper.Map<HomeSpecialityViewModel>(this.specialityService.GetSpeciality(id.Value));

            if (existingSpeciality == null)
            {
                return this.HttpNotFound("There is no such record.");
            }

            existingSpeciality.Doctors =
                new PagedList<SpecialityDoctorViewModel>(existingSpeciality.Doctors, page, ItemPerPage);

            return View(existingSpeciality);
        }
    }
}
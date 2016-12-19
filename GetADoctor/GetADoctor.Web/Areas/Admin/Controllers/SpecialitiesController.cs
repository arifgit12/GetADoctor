using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetADoctor.Models;
using GetADoctor.Data.Services;
using GetADoctor.Web.Areas.Admin.Models;

namespace GetADoctor.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialityService _specialityservice;

        public SpecialitiesController(ISpecialityService specialityservice)
        {
            this._specialityservice = specialityservice;
        }

        // GET: Admin/Specialities
        public ActionResult Index()
        {
            var speciliaties = this._specialityservice.GetSpecialities();
             var model = AutoMapper.Mapper.Map<IEnumerable<SpecialityViewModel>>(speciliaties);
            return View(model);
        }

        // GET: Admin/Specialities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = this._specialityservice.GetSpeciality(id.Value);
            if (speciality == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<SpecialityViewModel>(speciality);
            return View(model);
        }

        // GET: Admin/Specialities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Specialities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpecialityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var speciality = AutoMapper.Mapper.Map<Speciality>(model);
                speciality.CreatedOn = DateTime.UtcNow;
                speciality.UpdatedOn = DateTime.UtcNow;
                var isSave = this._specialityservice.SaveSpeciality(speciality);
                if(isSave > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: Admin/Specialities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = this._specialityservice.GetSpeciality(id.Value);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            var model = AutoMapper.Mapper.Map<SpecialityViewModel>(speciality);
            return View(model);
        }

        // POST: Admin/Specialities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecialityViewModel model)
        {
            if (ModelState.IsValid)
            {
                Speciality speciality = this._specialityservice.GetSpeciality(model.Id);
                speciality = AutoMapper.Mapper.Map(model, speciality);
                speciality.UpdatedOn = DateTime.UtcNow;
                var isUpdate = this._specialityservice.UpdateSpeciality(speciality);
                if(isUpdate > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: Admin/Specialities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = this._specialityservice.GetSpeciality(id.Value);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            var model = AutoMapper.Mapper.Map<SpecialityViewModel>(speciality);
            return View(model);
        }

        // POST: Admin/Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Speciality speciality = this._specialityservice.GetSpeciality(id);
            // Delete speciality - not implemented
            return RedirectToAction("Index");
        }
    }
}

using GetADoctor.Data.Services;
using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas.Admin.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService cityservice;
        private readonly IStateService stateService;
        public CitiesController(ICityService cityservice, IStateService stateService)
        {
            this.cityservice = cityservice;
            this.stateService = stateService;
        }
        // GET: Admin/Cities
        public ActionResult Index()
        {
            var cities = AutoMapper.Mapper.Map<IEnumerable<CityViewModel>>( cityservice.GetCities() );

            return View(cities);
        }

        // GET: Admin/Cities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Cities/Create
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(this.stateService.GetStates(), "Id", "StateName");
            return View();
        }

        // POST: Admin/Cities/Create
        [HttpPost]
        public ActionResult Create(CityViewModel model)
        {
            ViewBag.StateId = new SelectList(this.stateService.GetStates(), "Id", "StateName");
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var city = AutoMapper.Mapper.Map<City>(model);
                    city.CreatedOn = DateTime.UtcNow;
                    city.UpdatedOn = DateTime.Now;

                    var isSave = cityservice.SaveCity(city);
                    if(isSave > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }            
        }

        // GET: Admin/Cities/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Cities/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Cities/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Cities/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

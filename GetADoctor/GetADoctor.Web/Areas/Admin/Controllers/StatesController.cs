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
    public class StatesController : Controller
    {
        private readonly IStateService stateService;

        public StatesController(IStateService stateService)
        {
            this.stateService = stateService;
        }
        // GET: Admin/States
        public ActionResult Index()
        {
            var states = stateService.GetStates();
            return View(states);
        }

        // GET: Admin/States/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/States/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/States/Create
        [HttpPost]
        public ActionResult Create(StateViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                State state = new State();
                state.StateName = "WestBengal";
                state.CreatedOn = DateTime.Now;
                state.UpdatedOn = DateTime.Now;

                var isSave = stateService.SaveState(state);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/States/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/States/Edit/5
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

        // GET: Admin/States/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/States/Delete/5
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    [Authorize]
    public class SpecialitiesController : Controller
    {
        // GET: Specialities
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult All(int page = 1)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get(int? id, int page = 1)
        {
            return View();
        }
    }
}
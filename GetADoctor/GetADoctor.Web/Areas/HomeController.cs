using GetADoctor.Data.Infrastructure;
using GetADoctor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Results(string search)
        {
            ViewData["searchString"] = search;
            search = search.Trim();
            if (search.IndexOf(' ') != -1)
            {
                search = search.Replace(" ", "|");
            }

            Regex regex = new Regex(search, RegexOptions.IgnoreCase);

            var results = AutoMapper.Mapper.Map<IEnumerable<DoctorSearchViewModel>>(db.Doctors.ToList())
                .Where(d => regex.IsMatch(d.FirstName.ToString()) | regex.IsMatch(d.LastName.ToString()));

            if (this.Request.IsAjaxRequest())
            {
                if (search.Length > 2)
                {
                    var model = results.OrderBy(d => d.LastName).Take(5).ToList();
                    return this.PartialView("_AjaxResults", model);
                }
                else
                {
                    return this.Content("<span class=\"list-group-item\">Insufficient Length</span>");
                }

            }

            return this.View(results);
        }
    }
}
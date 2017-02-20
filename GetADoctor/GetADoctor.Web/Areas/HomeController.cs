using GetADoctor.Data.Infrastructure;
using GetADoctor.Data.Services;
using GetADoctor.Web.Models;
using GetADoctor.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    public class HomeController : BaseController
    {
        private readonly IDoctorService doctorService;
        public HomeController(ApplicationUserManager userManager, IDoctorService doctorservice)
            : base(userManager)
        {
            this.doctorService = doctorservice;
        }

        //private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var homeTables = new HomeTablesViewModel();

            var mostCommented =
               this.doctorService
               .GetDoctors()
               .OrderByDescending(d => d.Comments.Count)
               .Take(5);
            homeTables.MostCommented = AutoMapper.Mapper.Map<IEnumerable< HomeDoctorViewModel>>(mostCommented);

            var highestRating =
              this.doctorService
               .GetDoctors()
              .Where(d => d.Rating.Count > 4)
              .OrderByDescending(d => (float)d.Rating.Sum(r => r.Value) / d.Rating.Count)
              .Take(5);
            homeTables.HighestRating = AutoMapper.Mapper.Map<IEnumerable<HomeDoctorViewModel>>(highestRating);

            return View(homeTables);
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

            var results = AutoMapper.Mapper.Map<IEnumerable<DoctorSearchViewModel>>(this.doctorService
               .GetDoctors().ToList())
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
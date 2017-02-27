using GetADoctor.Data.Services;
using GetADoctor.Web.Models;
using GetADoctor.Web.Models.Doctors;
using GetADoctor.Web.Models.Home;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace GetADoctor.Web.Areas
{
    public class HomeController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly ICityService cityService;
        private const int ItemsPerPage = 10;
        public HomeController() { }
        public HomeController(IDoctorService doctorservice, ICityService cityService)
        {
            this.doctorService = doctorservice;
            this.cityService = cityService;
        }

        public ActionResult Index()
        {
            var citiesList = this.cityService
                .GetCities()
                .Select(c => new SelectListItem { Text = c.CityName, Value = c.CityId.ToString() })
                .ToList();
            var selectedAll = new SelectListItem { Text = "All", Value = "" };
            citiesList.Insert(0, selectedAll);

            var specialitiesList = this.doctorService
                .GetSpecialities()
                .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() })
                .ToList();
            specialitiesList.Insert(0, selectedAll);

            var homeTables = new HomeTablesViewModel();

            homeTables.Specialities = specialitiesList;
            homeTables.Cities = citiesList;

            var mostCommented =
               this.doctorService
               .GetDoctors()
               .OrderByDescending(d => d.Comments.Count)
               .Take(5);
            homeTables.MostCommented = AutoMapper.Mapper.Map<IEnumerable<HomeDoctorViewModel>>(mostCommented);

            var highestRating =
              this.doctorService
               .GetDoctors()
              .Where(d => d.Rating.Count > 4)
              .OrderByDescending(d => (float)d.Rating.Sum(r => r.Value) / d.Rating.Count)
              .Take(5);
            homeTables.HighestRating = AutoMapper.Mapper.Map<IEnumerable<HomeDoctorViewModel>>(highestRating);

            return View(homeTables);
        }

        public ActionResult SearchBest(int page = 1, string name = null, int? speciality = null, string search = null)
        {
            if( !string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                if (name.IndexOf(' ') != -1)
                {
                    name = name.Replace(" ", "|");
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                if (search.IndexOf(' ') != -1)
                {
                    search = search.Replace(" ", "|");
                }
            }

            var doctors = this.doctorService.SearchDoctors(name, speciality, search);

            var docmodel = AutoMapper.Mapper.Map<IEnumerable<DoctorViewModel>>(doctors);

            var selectedAll = new SelectListItem { Text = "All", Value = "" };

            var specialitiesList = this.doctorService
               .GetSpecialities()
               .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() })
               .ToList();
            specialitiesList.Insert(0, selectedAll);


            var citiesList = this.cityService
                .GetCities()
                .Select(c => new SelectListItem { Text = c.CityName, Value = c.CityId.ToString() })
                .ToList();
            citiesList.Insert(0, selectedAll);

            var model = new FilterDoctorsViewModel()
            {
                Doctors = new PagedList<DoctorViewModel>(docmodel, page, ItemsPerPage),
                Cities = citiesList,
                Specialities = specialitiesList
            };

            return View(model);
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
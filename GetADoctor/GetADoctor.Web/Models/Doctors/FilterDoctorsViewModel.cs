namespace GetADoctor.Web.Models.Doctors
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using PagedList;
    public class FilterDoctorsViewModel
    {
        public PagedList<DoctorViewModel> Doctors { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Specialities { get; set; }
    }
}
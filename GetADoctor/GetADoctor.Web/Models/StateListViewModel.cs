using GetADoctor.Models;
using GetADoctor.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetADoctor.Web.Models
{
    public class StateListViewModel
    {
        public List<State> StateList = new List<State>();
        public int Id { get; set; }

        public IEnumerable<SelectListItem> StateEnum
        {
            get
            {
                return new SelectList(StateList, "Id", "StateName");
            }
        }
    }
}
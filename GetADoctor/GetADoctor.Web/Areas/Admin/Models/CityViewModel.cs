using GetADoctor.Models;
using System.ComponentModel.DataAnnotations;

namespace GetADoctor.Web.Areas.Admin.Models
{
    public class CityViewModel : SystemEntity
    {
        public int CityId { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "City Name")]
        public string CityName { get; set; }        

        [Display(Name = "State Name")]
        public string StateName { get; set; }

        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}
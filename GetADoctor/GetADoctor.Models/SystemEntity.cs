using System;
using System.ComponentModel.DataAnnotations;

namespace GetADoctor.Models
{
    public abstract class SystemEntity
    {        
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

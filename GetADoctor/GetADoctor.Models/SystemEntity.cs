using System;
using System.ComponentModel.DataAnnotations;

namespace GetADoctor.Models
{
    public class SystemEntity
    {
        [Key]
        public int Id { get; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

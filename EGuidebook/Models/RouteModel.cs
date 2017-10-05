using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    [Table("Routes")]
    public class RouteModel
    {
        [Key]
        public Guid RouteID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [ForeignKey("SpotID")]
        public List<SpotModel> Spots { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    [Table("SpotsRoutes")]
    public class SpotsRoutesModel
    {
        [Key, Column(Order = 0), ForeignKey("Spot")]
        public Guid SpotID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Route")]
        public Guid RouteID { get; set; }

        public int Ordinal { get; set; }

        public SpotModel Spot { get; set; }
        public RouteModel Route { get; set; }
    }
}
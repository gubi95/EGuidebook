using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class RouteViewModel
    {
        public Guid RouteID { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Trasa systemowa")]
        public bool IsSystemRoute { get; set; }

        public string SpotsIDsListFormatted { get; set; }
        public List<HomeViewModel.SpotCoordinates> ApplicationSpots { get; set; }

        public RouteViewModel() { }

        public RouteViewModel(RouteModel objRouteModel)
        {
            this.RouteID = objRouteModel.RouteID;
            this.Name = objRouteModel.Name;
            this.Description = objRouteModel.Description;
            this.IsSystemRoute = objRouteModel.IsSystemRoute;
        }

        public RouteViewModel(RouteModel objRouteModel, List<SpotModel> listApplicationSpots, bool bIsCreate)
        {
            this.RouteID = objRouteModel.RouteID;
            this.Name = objRouteModel.Name;
            this.Description = objRouteModel.Description;
            this.IsSystemRoute = objRouteModel.IsSystemRoute;
            this.ApplicationSpots = listApplicationSpots != null ? listApplicationSpots.Select(x => new HomeViewModel.SpotCoordinates(x)).ToList() : new List<HomeViewModel.SpotCoordinates>();
            
            if(bIsCreate)
            {
                this.SpotsIDsListFormatted = "";
            }
            else
            {
                this.SpotsIDsListFormatted = string.Join(",", objRouteModel.Spots.OrderBy(x => x.Ordinal).Select(x => x.SpotID.ToString()));
            }
        }
    }
}
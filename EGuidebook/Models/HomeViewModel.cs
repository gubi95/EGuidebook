using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class HomeViewModel
    {
        public int UsersCount { get; set; }
        public int SpotsCount { get; set; }
        public int SpotCategoriesCount { get; set; }
        public List<SpotCoordinates> SpotCoordinatesList { get; set; }

        public class SpotCoordinates
        {
            public string SpotID { get; set; }
            public double CoorX { get; set; }
            public double CoorY { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> ImagePaths { get; set; }

            public SpotCoordinates() { }

            public SpotCoordinates(SpotModel objSpotModel)
            {
                this.SpotID = objSpotModel.SpotID.ToString();
                this.CoorX = double.Parse(objSpotModel.CoorX, System.Globalization.CultureInfo.InvariantCulture);
                this.CoorY = double.Parse(objSpotModel.CoorY, System.Globalization.CultureInfo.InvariantCulture);
                this.Name = objSpotModel.Name;
                this.Description = objSpotModel.Description;
                this.ImagePaths = new List<string>()
                {
                    objSpotModel.Image1Path,
                    objSpotModel.Image2Path,
                    objSpotModel.Image3Path,
                    objSpotModel.Image4Path,
                    objSpotModel.Image5Path
                }
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            }
        }
    }
}
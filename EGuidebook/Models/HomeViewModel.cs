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
            public double CoorX { get; set; }
            public double CoorY { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> ImagePaths { get; set; }
        }
    }
}
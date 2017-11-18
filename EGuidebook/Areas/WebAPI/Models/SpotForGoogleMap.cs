using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGuidebook.Models;

namespace EGuidebook.Areas.WebAPI.Models
{
    public class SpotForGoogleMap
    {
        public string SpotID { get; private set; }
        public string Name { get; private set; }
        public double CoorX { get; private set; }
        public double CoorY { get; private set; }

        public SpotForGoogleMap(string strSpotID, string strName, double dCoorX, double dCoorY)
        {
            this.SpotID = strSpotID;
            this.Name = strName;
            this.CoorX = dCoorX;
            this.CoorY = dCoorY;
        }
    }
}
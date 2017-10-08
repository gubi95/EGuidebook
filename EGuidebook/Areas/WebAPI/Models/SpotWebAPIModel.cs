using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Areas.WebAPI.Models
{
    public class SpotWebAPIModel
    {
        public string SpotID { get; set; }
        public string SpotCategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CoorX { get; set; }
        public double CoorY { get; set; }        
        public string Image1Path { get; set; }
        public string Image2Path { get; set; }
        public string Image3Path { get; set; }
        public string Image4Path { get; set; }
        public string Image5Path { get; set; }
        public int AverageGrade { get; set; }

        public SpotWebAPIModel(EGuidebook.Models.SpotModel objSpotModel)
        {
            this.SpotID = objSpotModel.SpotID.ToString();
            this.SpotCategoryID = objSpotModel.SpotCategoryID.ToString();
            this.Name = objSpotModel.Name;
            this.Description = objSpotModel.Description;
            this.CoorX = double.Parse(objSpotModel.CoorX, System.Globalization.CultureInfo.InvariantCulture);
            this.CoorY = double.Parse(objSpotModel.CoorY, System.Globalization.CultureInfo.InvariantCulture);
            this.Image1Path = "" + objSpotModel.Image1Path;
            this.Image2Path = "" + objSpotModel.Image2Path;
            this.Image3Path = "" + objSpotModel.Image3Path;
            this.Image4Path = "" + objSpotModel.Image4Path;
            this.Image5Path = "" + objSpotModel.Image5Path;

            if(objSpotModel.Grades != null && objSpotModel.Grades.Count > 0)
            {
                this.AverageGrade = (int) Math.Ceiling(objSpotModel.Grades.Average(x => x.Grade));
            }
            else
            {
                this.AverageGrade = 0;
            }
        }
    }
}
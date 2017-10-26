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
        public int UserGrade { get; set; }

        public bool IsOpeningHoursDefined { get; set; }
        public string OpeningHours_MondayFrom { get; set; }
        public string OpeningHours_MondayTo { get; set; }
        public string OpeningHours_TuesdayFrom { get; set; }
        public string OpeningHours_TuesdayTo { get; set; }
        public string OpeningHours_WednesdayFrom { get; set; }
        public string OpeningHours_WednesdayTo { get; set; }
        public string OpeningHours_ThursdayFrom { get; set; }
        public string OpeningHours_ThursdayTo { get; set; }
        public string OpeningHours_FridayFrom { get; set; }
        public string OpeningHours_FridayTo { get; set; }
        public string OpeningHours_SaturdayFrom { get; set; }
        public string OpeningHours_SaturdayTo { get; set; }
        public string OpeningHours_SundayFrom { get; set; }
        public string OpeningHours_SundayTo { get; set; }

        public decimal TicketKidsPrice { get; set; }
        public decimal TicketStudentPrice { get; set; }
        public decimal TicketAdultPrice { get; set; }

        public SpotWebAPIModel(EGuidebook.Models.SpotModel objSpotModel, EGuidebook.Models.SpotGradeModel objSpotGradeModel)
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

            if(
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_MondayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_MondayTo) &&
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_TuesdayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_TuesdayTo) &&
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_WednesdayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_WednesdayTo) &&
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_ThursdayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_ThursdayTo) &&
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_FridayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_FridayTo) &&
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_SaturdayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_SaturdayTo) &&
                !string.IsNullOrEmpty(objSpotModel.OpeningHours_SundayFrom) && !string.IsNullOrEmpty(objSpotModel.OpeningHours_SundayTo))
            {
                this.IsOpeningHoursDefined = true;
                this.OpeningHours_MondayFrom = objSpotModel.OpeningHours_MondayFrom;
                this.OpeningHours_MondayTo = objSpotModel.OpeningHours_MondayTo;
                this.OpeningHours_TuesdayFrom = objSpotModel.OpeningHours_TuesdayFrom;
                this.OpeningHours_TuesdayTo = objSpotModel.OpeningHours_TuesdayTo;
                this.OpeningHours_WednesdayFrom = objSpotModel.OpeningHours_WednesdayFrom;
                this.OpeningHours_WednesdayTo = objSpotModel.OpeningHours_WednesdayTo;
                this.OpeningHours_ThursdayFrom = objSpotModel.OpeningHours_ThursdayFrom;
                this.OpeningHours_ThursdayTo = objSpotModel.OpeningHours_ThursdayTo;
                this.OpeningHours_FridayFrom = objSpotModel.OpeningHours_FridayFrom;
                this.OpeningHours_FridayTo = objSpotModel.OpeningHours_FridayTo;
                this.OpeningHours_SaturdayFrom = objSpotModel.OpeningHours_SaturdayFrom;
                this.OpeningHours_SaturdayTo = objSpotModel.OpeningHours_SaturdayTo;
                this.OpeningHours_SundayFrom = objSpotModel.OpeningHours_SundayFrom;
                this.OpeningHours_SundayTo = objSpotModel.OpeningHours_SundayTo;
            }
            else
            {
                this.IsOpeningHoursDefined = false;
            }

            this.UserGrade = objSpotGradeModel != null ? objSpotGradeModel.Grade : -1;

            this.TicketKidsPrice = objSpotModel.TicketKidsPrice;
            this.TicketStudentPrice = objSpotModel.TicketStudentPrice;
            this.TicketAdultPrice = objSpotModel.TicketAdultPrice;
        }
    }
}
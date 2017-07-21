using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class SpotViewModel
    {
        [DisplayName("Nazwa:")]
        [Required(ErrorMessage = "Proszę wpisać nazwę miejsca")]
        public string Name { get; set; }
        [DisplayName("Opis:")]
        [Required(ErrorMessage = "Proszę wpisać opis miejsca")]
        public string Description { get; set; }
        [DisplayName("Współrzędna X:")]
        [RegularExpression(@"^(\+|-)?(?:90(?:(?:\.0{1,7})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,7})?))$", ErrorMessage = "Niepoprawny format współrzędnej.")]
        [Required(ErrorMessage = "Proszę wpisać współrzędną X miejsca")]
        public string CoorX { get; set; }
        [DisplayName("Współrzędna Y:")]
        [RegularExpression(@"^(\+|-)?(?:180(?:(?:\.0{1,7})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,7})?))$", ErrorMessage = "Niepoprawny format współrzędnej")]
        [Required(ErrorMessage = "Proszę wpisać współrzędną Y miejsca")]
        public string CoorY { get; set; }

        [DisplayName("Zdjęcia:")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image1 { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image2 { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image3 { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image4 { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image5 { get; set; }

        public void ApplyFromModel(SpotModel objSpotModel)
        {
            this.Name = objSpotModel.Name;
            this.Description = objSpotModel.Description;
            this.CoorX = objSpotModel.CoorX;
            this.CoorY = objSpotModel.CoorY;
        }
    }
}
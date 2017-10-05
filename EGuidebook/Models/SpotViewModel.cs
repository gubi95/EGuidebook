using EGuidebook.Shared;
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
        public Guid SpotID { get; set; }

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

        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public ApplicationUser ApprovedBy { get; set; }

        [DisplayName("Oceny")]
        public List<SpotGradeViewModel> Grades { get; set; }

        [DisplayName("Zdjęcia:")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image1 { get; set; }
        public string Image1Path { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image2 { get; set; }
        public string Image2Path { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image3 { get; set; }
        public string Image3Path { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image4 { get; set; }
        public string Image4Path { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image5 { get; set; }
        public string Image5Path { get; set; }

        [DisplayName("Kategoria:")]
        public string SpotCategoryId { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_MondayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_MondayTo{ get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_TuesdayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_TuesdayTo { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_WednesdayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_WednesdayTo { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_ThursdayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_ThursdayTo { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_FridayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_FridayTo { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_SaturdayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_SaturdayTo { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_SundayFrom { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string OpeningHours_SundayTo { get; set; }

        [Display(Name = "Dorośli:")]
        [RegularExpression("^[0-9]+([.,][0-9]{1,2})?$", ErrorMessage = "Nieprawidłowa wartość")]
        public string TicketAdultPrice { get; set; }
        [Display(Name = "Studenci:")]
        [RegularExpression("^[0-9]+([.,][0-9]{1,2})?$", ErrorMessage = "Nieprawidłowa wartość")]
        public string TicketStudentPrice { get; set; }
        [Display(Name = "Dzieci:")]
        [RegularExpression("^[0-9]+([.,][0-9]{1,2})?$", ErrorMessage = "Nieprawidłowa wartość")]
        public string TicketKidsPrice { get; set; }

        public SpotViewModel() { }
        public SpotViewModel(SpotModel objSpotModel)
        {
            this.ApplyFromModel(objSpotModel);
        }

        public void ApplyFromModel(SpotModel objSpotModel)
        {
            this.SpotID = objSpotModel.SpotID;
            this.Name = objSpotModel.Name;
            this.Description = objSpotModel.Description;
            this.SpotCategoryId = objSpotModel.SpotCategoryID.ToString();
            this.CoorX = objSpotModel.CoorX;
            this.CoorY = objSpotModel.CoorY;
            this.IsApproved = objSpotModel.IsApproved;
            this.ApprovalDate = objSpotModel.ApprovalDate;
            this.ApprovedBy = objSpotModel.ApprovedBy;
            this.Grades = objSpotModel.Grades != null ? objSpotModel.Grades.Select(x => new SpotGradeViewModel(x)).ToList() : new List<SpotGradeViewModel>();

            this.Image1Path = objSpotModel.Image1Path;
            this.Image2Path = objSpotModel.Image2Path;
            this.Image3Path = objSpotModel.Image3Path;
            this.Image4Path = objSpotModel.Image4Path;
            this.Image5Path = objSpotModel.Image5Path;

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

            this.TicketAdultPrice = objSpotModel.TicketAdultPrice.ToString();
            this.TicketKidsPrice = objSpotModel.TicketKidsPrice.ToString();
            this.TicketStudentPrice = objSpotModel.TicketStudentPrice.ToString();
        }

        public void ApplyToModel(ref SpotModel objSpotModel, bool bIsCreate)
        {
            objSpotModel.Name = this.Name;
            objSpotModel.Description = this.Description;
            objSpotModel.SpotCategoryID = new Guid(this.SpotCategoryId);
            objSpotModel.CoorX = this.CoorX;
            objSpotModel.CoorY = this.CoorY;

            objSpotModel.OpeningHours_MondayFrom = this.OpeningHours_MondayFrom;
            objSpotModel.OpeningHours_MondayTo = this.OpeningHours_MondayTo;
            objSpotModel.OpeningHours_TuesdayFrom = this.OpeningHours_TuesdayFrom;
            objSpotModel.OpeningHours_TuesdayTo = this.OpeningHours_TuesdayTo;
            objSpotModel.OpeningHours_WednesdayFrom = this.OpeningHours_WednesdayFrom;
            objSpotModel.OpeningHours_WednesdayTo = this.OpeningHours_WednesdayTo;
            objSpotModel.OpeningHours_ThursdayFrom = this.OpeningHours_ThursdayFrom;
            objSpotModel.OpeningHours_ThursdayTo = this.OpeningHours_ThursdayTo;
            objSpotModel.OpeningHours_FridayFrom = this.OpeningHours_FridayFrom;
            objSpotModel.OpeningHours_FridayTo = this.OpeningHours_FridayTo;
            objSpotModel.OpeningHours_SaturdayFrom = this.OpeningHours_SaturdayFrom;
            objSpotModel.OpeningHours_SaturdayTo = this.OpeningHours_SaturdayTo;
            objSpotModel.OpeningHours_SundayFrom = this.OpeningHours_SundayFrom;
            objSpotModel.OpeningHours_SundayTo = this.OpeningHours_SundayTo;

            try
            {
                objSpotModel.TicketAdultPrice = decimal.Parse(("" + this.TicketAdultPrice).Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                objSpotModel.TicketAdultPrice = 0.00M;
            }

            try
            {
                objSpotModel.TicketKidsPrice = decimal.Parse(("" + this.TicketKidsPrice).Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                objSpotModel.TicketKidsPrice = 0.00M;
            }

            try
            {
                objSpotModel.TicketStudentPrice = decimal.Parse(("" + this.TicketStudentPrice).Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                objSpotModel.TicketStudentPrice = 0.00M;
            }

            if(bIsCreate)
            {
                objSpotModel.Grades = new List<SpotGradeModel>();
            }

            if (bIsCreate)
            {
                objSpotModel.Image1Path = Helper.SaveSpotImage(this.Image1);
                objSpotModel.Image2Path = Helper.SaveSpotImage(this.Image2);
                objSpotModel.Image3Path = Helper.SaveSpotImage(this.Image3);
                objSpotModel.Image4Path = Helper.SaveSpotImage(this.Image4);
                objSpotModel.Image5Path = Helper.SaveSpotImage(this.Image5);
            }
            else
            {
                objSpotModel.Image1Path = this.updateSpotImage(objSpotModel.Image1Path, this.Image1Path, this.Image1);
                objSpotModel.Image2Path = this.updateSpotImage(objSpotModel.Image2Path, this.Image2Path, this.Image2);
                objSpotModel.Image3Path = this.updateSpotImage(objSpotModel.Image3Path, this.Image3Path, this.Image3);
                objSpotModel.Image4Path = this.updateSpotImage(objSpotModel.Image4Path, this.Image4Path, this.Image4);
                objSpotModel.Image5Path = this.updateSpotImage(objSpotModel.Image5Path, this.Image5Path, this.Image5);
            }

            this.reorderImages(ref objSpotModel);
        }

        private string updateSpotImage(string strCurrentImagePathFromModel, string strCurrentImagePathFromViewModel, HttpPostedFileBase objHttpPostedFileBase)
        {
            string strRetValue = strCurrentImagePathFromModel;

            if (string.IsNullOrEmpty(strCurrentImagePathFromViewModel))
            {
                Helper.DeleteFile(strCurrentImagePathFromModel);
                strRetValue = null;
            }

            if (objHttpPostedFileBase != null && objHttpPostedFileBase.ContentLength > 0)
            { 
                strRetValue = Helper.SaveSpotImage(objHttpPostedFileBase);
            }

            return strRetValue;
        }

        private void reorderImages(ref SpotModel objSpotModel)
        {
            List<string> listPaths = new List<string>();
            if (!string.IsNullOrEmpty(objSpotModel.Image1Path)) listPaths.Add(objSpotModel.Image1Path);
            if (!string.IsNullOrEmpty(objSpotModel.Image2Path)) listPaths.Add(objSpotModel.Image2Path);
            if (!string.IsNullOrEmpty(objSpotModel.Image3Path)) listPaths.Add(objSpotModel.Image3Path);
            if (!string.IsNullOrEmpty(objSpotModel.Image4Path)) listPaths.Add(objSpotModel.Image4Path);
            if (!string.IsNullOrEmpty(objSpotModel.Image5Path)) listPaths.Add(objSpotModel.Image5Path);

            int i = 0;
            for (; i < listPaths.Count; i++)
            {
                typeof(SpotModel).GetProperty("Image" + (i + 1) + "Path").SetValue(objSpotModel, listPaths[i], null);
            }
            
            for(; i < 5; i++)
            {
                typeof(SpotModel).GetProperty("Image" + (i + 1) + "Path").SetValue(objSpotModel, null, null);
            }
        }

        private bool isFileUploadFilled(HttpPostedFileBase objHttpPostedFileBase)
        {
            return objHttpPostedFileBase != null && objHttpPostedFileBase.ContentLength > 0;
        }
    }
}
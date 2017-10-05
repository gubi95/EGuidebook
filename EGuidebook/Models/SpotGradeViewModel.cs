using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class SpotGradeViewModel
    {
        public int Grade { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public UserViewModel User { get; set; }

        public SpotGradeViewModel(SpotGradeModel objSpotGradeModel)
        {
            this.Grade = objSpotGradeModel.Grade;
            this.Message = objSpotGradeModel.Message;
            this.CreationDate = objSpotGradeModel.CreationDate;

            if (objSpotGradeModel.User != null)
            {
                this.User = new UserViewModel(objSpotGradeModel.User);
            }
        }
    }
}
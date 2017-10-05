using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGuidebook.Models;
using EGuidebook.Shared;

namespace EGuidebook.Areas.WebAPI
{
    public class SpotCategoryWebAPIModel
    {
        public Guid SpotCategoryId { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }

        public SpotCategoryWebAPIModel(SpotCategoryModel objSpotCategoryModel)
        {
            this.SpotCategoryId = objSpotCategoryModel.SpotCategoryId;
            this.Name = objSpotCategoryModel.Name;
            string strIconPath = Helper.CheckIfFileExists(objSpotCategoryModel.IconPath) ? objSpotCategoryModel.IconPath : Helper.NoPhotoImgPath;
            this.IconPath = strIconPath;
        }
    }
}
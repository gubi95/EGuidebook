using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class SpotCategoryViewModel
    {
        public Guid SpotCategoryId { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Ikona")]
        public string IconPath { get; set; }

        public SpotCategoryViewModel() { }
        public SpotCategoryViewModel(SpotCategoryModel objSpotCategoryModel)
        {
            this.SpotCategoryId = objSpotCategoryModel.SpotCategoryId;
            this.Name = objSpotCategoryModel.Name;
            this.IconPath = objSpotCategoryModel.IconPath;
        }

        public void ApplyOnModel(ref SpotCategoryModel objSpotCategoryModel)
        {
            objSpotCategoryModel.Name = this.Name;
            objSpotCategoryModel.IconPath = this.IconPath;
        }
    }
}
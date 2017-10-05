using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    [Table("SpotCategories")]
    public class SpotCategoryModel
    {
        [Key]
        public Guid SpotCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string IconPath { get; set; }
    }
}
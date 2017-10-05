using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    [Table("SpotGrades")]
    public class SpotGradeModel
    {
        [Key]
        public Guid SpotGradeID { get; set; }
        [Required] 
        public int Grade { get; set; }

        public string Message { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class SpotModel
    {
        [Key]
        public Guid SpotID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public string Description { get; set; }
        [Required]
        public float CoorX { get; set; }
        [Required]
        public float CoorY { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public int RecommendationCount { get; set; }
        
        public string ApprovedByUserID { get; set; }
        [ForeignKey("ApprovedByUserID")]
        public ApplicationUser ApprovedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
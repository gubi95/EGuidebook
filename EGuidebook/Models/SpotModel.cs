using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    [Table("Spots")]
    public class SpotModel
    {
        [Key]
        public Guid SpotID { get; set; }
        
        public Guid? SpotCategoryID { get; set; }
        [ForeignKey("SpotCategoryID")]
        public SpotCategoryModel SpotCategory { get; set; }

        [Required]
        public string Name { get; set; }         
        public string Description { get; set; }
        [Required]
        public string CoorX { get; set; }
        [Required]
        public string CoorY { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        
        public string ApprovedByUserID { get; set; }
        [ForeignKey("ApprovedByUserID")]
        public ApplicationUser ApprovedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public List<SpotGradeModel> Grades { get; set; }

        public string Image1Path { get; set; }
        public string Image2Path { get; set; }
        public string Image3Path { get; set; }
        public string Image4Path { get; set; }
        public string Image5Path { get; set; }
        
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
        
        public decimal TicketAdultPrice { get; set; }
        public decimal TicketStudentPrice { get; set; }
        public decimal TicketKidsPrice { get; set; }

        [ForeignKey("RouteID")]
        public List<RouteModel> Routes { get; set; }
    }
}
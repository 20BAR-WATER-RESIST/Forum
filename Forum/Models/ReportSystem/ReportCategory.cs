using System.ComponentModel.DataAnnotations;

namespace Forum.Models.ReportSystem
{
    public class ReportCategory
    {
        [Key]
        public int ReportCategoryID { get; set; }
        public string ReportCategoryName { get; set; }
        public string ReportCategoryTarget { get; set; }
        public bool IsActive { get; set; }
    }
}

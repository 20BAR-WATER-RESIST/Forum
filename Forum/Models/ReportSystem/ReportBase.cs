using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models.ReportSystem
{
    public class ReportBase
    {
        [Key]
        public int ReportID { get; set; }
        public string ReportType { get; set; }
        public string? ReportMessage { get; set; }
        public DateTime ReportAddedDate { get; set; }
        public DateTime? ReportClosedDate { get; set; }
        public string? ReportClosedBy { get; set; }
        public int? ReportTargetID { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ReportBaseReportedCommentAddedTime { get; set; }
        public string ConfirmedBanReason { get; set; }
        public bool? TargetUserAccountStatus { get; set; }
        public bool? TargetTopicAccountStatus { get; set; }
        public bool? TargetCommentAccountStatus { get; set; }
        public DateTime? UserBannedTime { get; set; }

        public int ReportCategoryID { get; set; }
        public ReportCategory ReportCategory { get; set; }
        public string? ReportAuthorName { get; set; }
        public int ReportAuthor { get; set; }
        public string? ReportTargetUsername { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}

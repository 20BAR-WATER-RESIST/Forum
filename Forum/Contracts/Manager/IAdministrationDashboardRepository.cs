using Forum.Models;
using Forum.Models.ReportSystem;

namespace Forum.Contracts.Manager
{
    public interface IAdministrationDashboardRepository
    {
        Task<string> BanReportTargetAuthor(int reportId, string reason, DateTime dateTime);
        Task DeleteReport(int reportId);
        Task LiftBanMethod(int userID);
        Task<List<User>> LoadBansEndingSoon();
        Task<Comment> loadReportedCommentData(int id);
        Task<List<ReportBase>> LoadReports();
        Task<string> QuickBanMethod(string reportedType, int reportedID);
    }
}

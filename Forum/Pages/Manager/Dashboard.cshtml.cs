using Forum.Contracts.Manager;
using Forum.Models;
using Forum.Models.ReportSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages.Manager
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly IAdministrationDashboardRepository _administrationDashboardRepository;

        public DashboardModel(IAdministrationDashboardRepository administrationDashboardRepository)
        {
            _administrationDashboardRepository = administrationDashboardRepository;
        }

        public List<ReportBase> loadReports { get; private set; }
        public List<User> loadBansEndingSoon { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            bool isAdmin = User.IsInRole("Admin");
            bool isOwner = User.IsInRole("Owner");

            if (isAdmin || isOwner)
            {
                loadReports = await _administrationDashboardRepository.LoadReports();
                loadBansEndingSoon = await _administrationDashboardRepository.LoadBansEndingSoon();
                return Page();
            }
            else
            {
                return new ForbidResult();
            }
        }

        public async Task<JsonResult> OnGetLoadReportedComment(int id)
        {
            var commentData = await _administrationDashboardRepository.loadReportedCommentData(id);

            // Tworzenie obiektu JSON do zwrócenia
            var result = new
            {
                author = commentData.CommentAuthor,
                date = commentData.CommentAddedTime.ToString("dd-MM-yyyy"),
                content = commentData.CommentText
            };

            return new JsonResult(result);
        }

        public async Task<JsonResult> OnPostDeleteReport([FromBody] ReportBase reportIdViewModel)
        {
            int reportId = reportIdViewModel.ReportID;
            if (reportId > 0)
            {
                await _administrationDashboardRepository.DeleteReport(reportId);
                return new JsonResult(new { success = true });
            }
            else
            {
                return new JsonResult(new { success = false });
            }
        }

        public async Task<JsonResult> OnPostBanUser([FromBody] ReportBase banUserData)
        {
            int userID = (int)banUserData.ReportTargetID;
            string banReason = banUserData.ConfirmedBanReason;
            DateTime dateTime = (DateTime)banUserData.UserBannedTime;

            if(userID > 0 && !string.IsNullOrEmpty(banReason))
            {
                await _administrationDashboardRepository.BanReportTargetAuthor(userID, banReason, dateTime);
                return new JsonResult(new { success = true, message = "Wszystko git" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "B³¹d po stronie aplikacji spróbuj ponownie za chwilê" });
            }
        }

        public async Task<JsonResult> OnPostQuickBan([FromBody] ReportBase banData)
        {
            string reportType = banData.ReportType;
            int reportTargetID = (int)banData.ReportTargetID;

            if (!string.IsNullOrEmpty(reportType) && reportTargetID > 0)
            {
                await _administrationDashboardRepository.QuickBanMethod(reportType, reportTargetID);
                return new JsonResult(new { success = true });
            }
            else { return new JsonResult(new { success = false }); }
        }

        public async Task<JsonResult> OnPostLiftBan([FromBody] ReportBase banData)
        {
            int reportTargetID = (int)banData.ReportTargetID;

            if (reportTargetID > 0)
            {
                await _administrationDashboardRepository.LiftBanMethod(reportTargetID);
                return new JsonResult(new { success = true });
            }
            else { return new JsonResult(new { success = false }); }
        }
    }
}

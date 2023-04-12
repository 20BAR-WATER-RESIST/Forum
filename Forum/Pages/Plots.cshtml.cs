using Forum.Contracts;
using Forum.Models;
using Forum.Models.ReportSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Forum.Pages
{
    public class PlotsModel : PageModel
    {
        private readonly ITopicRepository _topics;
        private readonly ICommentRepository _comments;
        private readonly ICRUD_Repository _crudRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlotsModel(ITopicRepository topics, ICommentRepository comments, ICRUD_Repository crudRepository,IUserRepository userRepository, IReportRepository reportRepository ,IHttpContextAccessor httpContextAccessor) {
            _topics = topics;
            _comments = comments;
            _crudRepository = crudRepository;
            _userRepository = userRepository;
            _reportRepository = reportRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Comment CommentFormData { get; set; }

        public int commentCounter { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 1;
        public int totalPages => (int)Math.Ceiling(decimal.Divide(commentCounter, 10));
        public bool showPrevious => currentPage > 1;
        public bool showNext => currentPage < totalPages;

        public List<Topic> plotsHolder { get; private set; }
        public List<Comment> commentsHolder { get; private set; }
        public List<ReportCategory> topicReportCategories { get; private set; }
        public List<ReportCategory> usersReportCategories { get; private set; }
        public List<ReportCategory> commentsReportCategories { get; private set; }

        public async Task OnGet(int id, int currentPage)
        {
            commentCounter = await _comments.GetCommentAmmountPerTopic(id);

            plotsHolder = await _topics.LoadPlot(id);
            commentsHolder = await _comments.LoadPlotComments(id, currentPage);
            topicReportCategories = await _topics.LoadTopicReportCategories();
            usersReportCategories = await _userRepository.LoadUsersReportCategories();
            commentsReportCategories = await _comments.LoadCommentsReportCategories();
        }

        public async Task<IActionResult> OnPostAddComment([FromRoute] int id)
        {
            string UserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            if (UserName != null)
            {
                await _crudRepository.InsertComment(CommentFormData.CommentText.ToString(), UserName, id);

                return RedirectToPage("/Plots", new { id, currentPage });
            }
            else { return Page(); }
        }

        public async Task<JsonResult> OnPostReportSender(ReportBase reportBase)
        {

            if (reportBase.ReportTargetID > 0 && !string.IsNullOrEmpty(reportBase.ReportAuthorName) && !string.IsNullOrEmpty(reportBase.ReportMessage))
            {
                var results = await _reportRepository.SendReport(reportBase.ReportAuthorName, reportBase.ReportCategoryID, reportBase.ReportMessage, (int)reportBase.ReportTargetID, reportBase.ReportType);
                return new JsonResult(new { success = true });
            }
            else
            {
                return new JsonResult(new { success = false });
            }
        }
    }
}

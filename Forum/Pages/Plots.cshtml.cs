using Forum.Contracts;
using Forum.Models;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlotsModel(ITopicRepository topics, ICommentRepository comments, ICRUD_Repository crudRepository, IHttpContextAccessor httpContextAccessor) {
            _topics = topics;
            _comments = comments;
            _crudRepository = crudRepository;
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

        //public List<(int TopicID, string TopicName, string TopicDescription, DateTime TopicAddedDate, int UserID, string UserName, int UserTypeID, string UserTypeName, int VotePlus, int VoteMinus)> plotsTopicData { get; private set; }
        //public List<(string CommentText, DateTime CommentAddedTime, bool IsActive, string UserName)> plotsCommentData { get; private set; }
        public List<Topic> dapperTest { get; private set; }
        public List<Comment> dapperComments { get; private set; }

        public async Task OnGet(int id, int currentPage)
        {
            commentCounter = await _comments.GetCommentAmmountPerTopic(id);
            //plotsTopicData = await _topics.LoadPlotTopic(id);
            //plotsCommentData = await _comments.LoadPlotsComments(id,currentPage);

            dapperTest = await _topics.LoadPlot(id);
            dapperComments = await _comments.LoadPlotComments(id, currentPage);
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
    }
}

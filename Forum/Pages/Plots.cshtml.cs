using Forum.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages
{
    public class PlotsModel : PageModel
    {
        private readonly ITopicRepository _topics;
        private readonly ICommentRepository _comments;

        public PlotsModel(ITopicRepository topics, ICommentRepository comments) {
            _topics = topics;
            _comments = comments;
        }

        public int commentCounter { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 1;
        public int totalPages => (int)Math.Ceiling(decimal.Divide(commentCounter, 10));
        public bool showPrevious => currentPage > 1;
        public bool showNext => currentPage < totalPages;

        public List<(string TopicName, string TopicDescription, DateTime TopicAddedDate, string TopicAuthor)> plotsTopicData { get; private set; }
        public List<(string CommentText, DateTime CommentAddedTime, bool IsActive, string UserName)> plotsCommentData { get; private set; }

        public async Task OnGet(int id, int currentPage)
        {
            commentCounter = await _comments.GetCommentAmmountPerTopic(id);
            plotsTopicData = await _topics.LoadPlotTopic(id);
            plotsCommentData = await _comments.LoadPlotsComments(id,currentPage);
        }
    }
}

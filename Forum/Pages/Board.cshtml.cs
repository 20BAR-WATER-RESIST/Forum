using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace Forum.Pages
{
    public class BoardModel : PageModel
    {
        private readonly ITopicRepository _topics;

        public BoardModel(ITopicRepository topics)
        {
            _topics = topics;
        }

        public int topicCounter { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 1;
        public int totalPages => (int)Math.Ceiling(decimal.Divide(topicCounter, 10));
        public bool showPrevious => currentPage > 1;
        public bool showNext => currentPage < totalPages;

        public List<(int CategoryID, int TopicID, string TopicName, string TopicAuthor, DateTime TopicAddedDate, int CommentCount, string CommentAuthor, DateTime CommentAddedTime)> boardData { get; private set; }

        public async Task OnGet(int id, string title, int currentPage)
        {
            topicCounter = await _topics.GetTopicAmmountPerCategory(id);
            boardData = await _topics.LoadBoard(id, currentPage);
        }
    }
}

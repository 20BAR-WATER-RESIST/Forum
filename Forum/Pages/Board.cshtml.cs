using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Forum.Pages
{
    public class BoardModel : PageModel
    {
        private readonly ITopicRepository<Topic> _topic;
        private readonly ICommentRepository<Comment> _comment;

        public BoardModel(ITopicRepository<Topic> topic, ICommentRepository<Comment> comment)
        {
            _topic = topic;
            _comment = comment;
        }

        internal IEnumerable<Topic> boardTopics { get; private set; }
        internal Dictionary<int, int> boardTopicCommentCount { get; private set; }

        public async Task OnGet(int id, string title)
        {
            boardTopics = _topic.TopicBoardLoader(id);
            boardTopicCommentCount = _comment.BoardTopicCommentCount(boardTopics);
        }
    }
}

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

        public List<(int CategoryID, int TopicID, string TopicName, string TopicAuthor, DateTime TopicAddedDate, int CommentCount, string CommentAuthor, DateTime CommentAddedTime)> boardData { get; private set; }

        public async Task OnGet(int id, string title)
        {
            boardData = await _topics.LoadBoard(id);
        }
    }
}

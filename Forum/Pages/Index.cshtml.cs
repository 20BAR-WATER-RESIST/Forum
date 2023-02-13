using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Forum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository<Category> _category;
        private readonly ITopicRepository<Topic> _topic;
        private readonly ICommentRepository<Comment> _comment;
        private readonly IUserRepository<User> _user;

        public IndexModel(ICategoryRepository<Category> category, ITopicRepository<Topic> topic, ICommentRepository<Comment> comment, IUserRepository<User> user)
        {
            _category = category;
            _topic = topic;
            _comment = comment;
            _user = user;
        }

        public IEnumerable<Category> justCategories { get; private set; }
        public IEnumerable<Topic> EachTopicRowOfCatID { get; private set; }
        public Dictionary<int, string> newestTopicsAuthors { get; private set; }
        public Dictionary<int, int> numberOfTopicsInCategory { get; private set; }
        public Dictionary<int, int> numberOfCommentsInCategory { get; private set; }

        public async Task OnGet()
        {
            justCategories = _category.GetAll();
            EachTopicRowOfCatID = _topic.EachTopicRowOfCategoryID(justCategories);
            newestTopicsAuthors = _user.NewestTopicsAuthors(EachTopicRowOfCatID);
            numberOfTopicsInCategory = _topic.TotalNumberOfTopics(justCategories);
            numberOfCommentsInCategory = _comment.CategoryCommentCounter(_topic.GetAll());
        }
    }
}
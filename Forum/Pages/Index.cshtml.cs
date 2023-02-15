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

        //internal List<User> Users = new List<User>();



        public IndexModel(ICategoryRepository<Category> category, ITopicRepository<Topic> topic, ICommentRepository<Comment> comment)
        {
            _category = category;
            _topic = topic;
            _comment = comment;

            //Users = _forumDbContext.Users
            //    .Include(u => u.Topics)
            //    .AsNoTracking()
            //    .ToList();

        }

        public IEnumerable<Category> justCategories { get; private set; }
        public IEnumerable<Topic> EachTopicRowOfCatID { get; private set; }
        public Dictionary<int, int> numbersOfTopicsInCategory { get; private set; }
        public Dictionary<int, int> numbersOfCommentsInTopics { get; private set; }
        public Category Findings { get; set; }

        public async Task OnGet()
        {
            justCategories = _category.GetAll();
            _category.LoadAllCategories();
            _topic.LoadAllTopics();
            EachTopicRowOfCatID = _topic.EachTopicRowOfCategoryID(justCategories);
            numbersOfTopicsInCategory = _topic.TotalNumberOfTopics(justCategories);
            numbersOfCommentsInTopics = _comment.TotalNumberOfComments(_topic.LoadAllTopics());
        }
    }
}
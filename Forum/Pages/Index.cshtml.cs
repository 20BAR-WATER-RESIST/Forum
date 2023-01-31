using Forum.Context;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Forum.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        //public void OnGet()
        //{

        //}

        private readonly ForumDbContext _forumDbContext;
        internal List<Category> Categories = new List<Category>();
        internal List<User> Users = new List<User>();

        public IndexModel(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
            Categories = _forumDbContext.Categories
                   .Include(s => s.Topics)
                   .ThenInclude(e => e.Comments)
                   .AsNoTracking()
                   .ToList();

            Users = _forumDbContext.Users
                .Include(u=>u.Topics)
                .AsNoTracking()
                .ToList();

        }

        //public IEnumerable<Category> Filter()
        //{
        //    var strcomm = from p in Categories
        //                  where p.Topics.Contains(Comment)
        //                  where
        //}



        public async Task OnGet()
        {
            
        }


    }
}
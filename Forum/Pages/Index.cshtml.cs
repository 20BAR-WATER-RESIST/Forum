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
       // internal List<Categories> users = new List<Categories>();

        public IndexModel(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        internal List<Category> Categories = new List<Category>();

        public async Task OnGet()
        {
            Categories = await _forumDbContext.Categories
                               .Include(s=>s.Topics)
                               .ThenInclude(e=>e.Comments)
                               .AsNoTracking()
                               .ToListAsync();
            
        }
    }
}
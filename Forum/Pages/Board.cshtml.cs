using Forum.Context;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Forum.Pages
{
    public class BoardModel : PageModel
    {
        private readonly DefaultDbContext _forumDbContext;

        internal List<Topic> Topics = new List<Topic>();

        public BoardModel(DefaultDbContext forumDbContext)
        {
            _forumDbContext= forumDbContext;
        }
        public async Task OnGet(int id, string title)
        {
            Topics = await _forumDbContext.Topics
                .Where(t=>t.CategoryID == id & t.Categories.CategoryName == title)
                .Include(t => t.Comments)
                .ThenInclude(t => t.User)
                .ToListAsync();
        }
    }
}

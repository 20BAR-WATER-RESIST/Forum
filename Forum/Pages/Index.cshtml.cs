using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.AccessControl;

namespace Forum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Contracts.ICategoryRepository _categories;

       public IndexModel(ICategoryRepository categories)
        {
            _categories = categories;
        }

        public List<(int CategoryID, string CategoryName, string CategoryDescription, string TopicName, string UserName, DateTime TopicAddedDate, int TotalTopicCount, int TotalCommentCount)> indexPageData { get; set; }


        public async Task OnGet()
        {
            indexPageData = await _categories.LoadEntireIndexPageData();
        }
    }
}
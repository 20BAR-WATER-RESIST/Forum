using Forum.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Contracts.ICategoryRepository _categories;
        private readonly IViewComponentHelper viewComponentHelper;

        public IndexModel(ICategoryRepository categories, IViewComponentHelper viewComponentHelper)
        {
            _categories = categories;
            this.viewComponentHelper = viewComponentHelper;
        }

        public List<(int CategoryID, string CategoryName, string CategoryDescription, string TopicName, string UserName, DateTime TopicAddedDate, int TotalTopicCount, int TotalCommentCount)> indexPageData { get; set; }


        public async Task OnGet()
        {
            indexPageData = await _categories.LoadEntireIndexPageData();
        }
    }
}
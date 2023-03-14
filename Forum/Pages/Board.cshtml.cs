using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace Forum.Pages
{
    public class BoardModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITopicRepository _topics;
        private readonly IBaseMethodsRepository _baseMethodsRepository;

        public BoardModel(ICategoryRepository categoryRepository, ITopicRepository topics, IBaseMethodsRepository baseMethodsRepository)
        {
            _categoryRepository = categoryRepository;
            _topics = topics;
            _baseMethodsRepository = baseMethodsRepository;
        }

        public async Task<string> TrimString(string description, int lenght)
        {
            return await _baseMethodsRepository.TrimString(description, lenght);
        }

        public async Task<string> Timer(TimeSpan dataTime)
        {
            return await _baseMethodsRepository.TopicTimer(dataTime);
        }

        public int routeCatID { get; private set; }

        public bool Active(int CategoryID)
        {
            return routeCatID == CategoryID;
        }

        
        public Category topicCounter { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 1;
        public int totalPages => (int)Math.Ceiling(decimal.Divide((decimal)topicCounter.TotalTopicCount, 10));
        public bool showPrevious => currentPage > 1;
        public bool showNext => currentPage < totalPages;

        public List<Topic> indexPageTopicData { get; private set; }
        public List<Category> indexPageCategoryData { get; private set; }
        public List<Topic> indexHotTopicsData { get; private set; }

        public async Task OnGet(int id, string title, int currentPage)
        {
            routeCatID = id;

            topicCounter = await _categoryRepository.GetTopicAmmountPerCategory(id);

            indexPageTopicData = await _topics.LoadBoardPageTopics(id, currentPage);
            indexPageCategoryData = await _categoryRepository.LoadIndexPageCategories();
            indexHotTopicsData = await _topics.BoardLatestHotTopics(id);
        }
    }
}

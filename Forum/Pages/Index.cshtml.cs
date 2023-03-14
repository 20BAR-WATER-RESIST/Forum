using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Forum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITopicRepository _topics;
        private readonly IBaseMethodsRepository _baseMethodsRepository;

        public IndexModel(ICategoryRepository categoryRepository,ITopicRepository topics, IBaseMethodsRepository baseMethodsRepository)
        {
            _categoryRepository = categoryRepository;
            _topics = topics;
            _baseMethodsRepository = baseMethodsRepository;
        }

        public List<Topic> indexPageTopicData { get; private set; }
        public List<Category> indexPageCategoryData { get; private set; }
        public List<Topic> indexHotTopicsData { get; private set; }

        public async Task<string> TrimString(string description, int lenght)
        {
            return await _baseMethodsRepository.TrimString(description, lenght);
        }

        public async Task<string> Timer(TimeSpan dataTime)
        {
            return await _baseMethodsRepository.TopicTimer(dataTime);
        }

        public async Task OnGet()
        {
            indexPageTopicData = await _topics.LoadIndexPageTopics();
            indexPageCategoryData = await _categoryRepository.LoadIndexPageCategories();
            indexHotTopicsData = await _topics.LatestHotTopics();
        }
    }
}
using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mysqlx.Session;
using System.Text.RegularExpressions;

namespace Forum.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IBaseMethodsRepository _baseMethodsRepository;

        public SearchModel(ITopicRepository topicRepository, IBaseMethodsRepository baseMethodsRepository)
        {
            _topicRepository = topicRepository;
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

        public string? Message { get; private set; }
        public List<Topic> topicSearchData { get; private set; }

        public async Task OnGet(string textContent)
        {
            var regex = new Regex("[^a-zA-Z0-9]");

            if (string.IsNullOrEmpty(textContent) || textContent.Length < 3 || regex.IsMatch(textContent))
            {
                Message = "Tekst wyszukiwania musi zawieraæ przynajmniej 3 znaków lub zawiera niedozwolone znaki specjalne";
            }
            else
            {
                topicSearchData = await _topicRepository.SearchTopics(textContent);

                if (topicSearchData.Count() == 0)
                {
                    Message = "Nie mamy tego czego szukasz :( spróbuj ponownie z inn¹ fraz¹ - dla testów polecam 'Superman' lub 'Auto' :)";
                }
            }
        }
    }
}

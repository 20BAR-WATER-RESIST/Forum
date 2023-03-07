using Forum.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Forum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Contracts.ICategoryRepository _categories;
        private readonly ITopicRepository _topics;

        public IndexModel(ICategoryRepository categories, ITopicRepository topics)
        {
            _categories = categories;
            _topics = topics;
        }

        public List<(int TopicID, string TopicName, string TopicDescription, int ViewCount, int CommentCount, TimeSpan TimeDiff)> indexData;

        public List<(int CategoryID, string CategoryName, int TotalTopicCount)> indexCatData { get; private set; }
        public List<(int TopicID, string TopicName, DateTime TopicAddedDate, int CommentCount)> indexHotTopicsData { get; private set; }

        public async Task<string> TrimString(string title)
        {
            string results = title.ToString();
            int maxLenght = 20;
            string backresults = results.Substring(0, Math.Min(results.Length, maxLenght));

            if (results.Length > maxLenght)
            {
                backresults += "...";
            }

            return backresults;
        }

        public async Task<string> TopicTimer(TimeSpan dataTime)
        {
            if (dataTime.TotalHours < 1)
            {
                int minutes = (int)dataTime.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "minute" : "minutes")}";
            }
            else if (dataTime.TotalHours < 24)
            {
                int hours = (int)dataTime.TotalHours;
                return $"{hours} {(hours == 1 ? "hour" : "hours")}";
            }
            else
            {
                int days = (int)dataTime.TotalDays;
                return $"{days} {(days == 1 ? "day" : "days")}+";
            }
        }

        public async Task OnGet()
        {
            indexData = await _topics.IndexPageBoardTopics();
            indexCatData = await _topics.IndexPageBoardCategories();
            indexHotTopicsData = await _topics.LatestHotTopics();
        }
    }
}
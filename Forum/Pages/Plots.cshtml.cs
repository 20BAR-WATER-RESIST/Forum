using Forum.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages
{
    public class PlotsModel : PageModel
    {
        private readonly ITopicRepository _topics;

        public PlotsModel(ITopicRepository topics) {
            _topics = topics;
        }

        public List<(int TopicID, string TopicName, string TopicDescription, DateTime TopicAddedDate, string TopicAuthor)> plotsTopicData { get; private set; }

        public async Task OnGet(int id)
        {
            plotsTopicData = await _topics.LoadPlotTopic(id);
        }
    }
}

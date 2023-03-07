using Forum.Models;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface ITopicRepository
    {
        Task<int> GetTopicAmmountPerCategory(int id);
        Task<List<(int CategoryID, string CategoryName, int TotalTopicCount)>> IndexPageBoardCategories();
        Task<List<(int TopicID, string TopicName, string TopicDescription, int ViewCount, int CommentCount, TimeSpan TimeDiff)>> IndexPageBoardTopics();
        Task<List<(int TopicID, string TopicName, DateTime TopicAddedDate, int CommentCount)>> LatestHotTopics();
        Task<List<(int CategoryID, int TopicID, string TopicName, string TopicAuthor, DateTime TopicAddedDate, int CommentCount, string CommentAuthor, DateTime CommentAddedTime)>> LoadBoard(int id, int currentPage);
        Task<List<(string TopicName, string TopicDescription, DateTime TopicAddedDate, string TopicAuthor)>> LoadPlotTopic(int id);
    }
}

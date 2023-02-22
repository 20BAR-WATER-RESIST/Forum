using Forum.Models;

namespace Forum.Contracts
{
    public interface ICategoryRepository
    {
        Task<List<(int CategoryID, string CategoryName, string CategoryDescription, string TopicName, string UserName, DateTime TopicAddedDate, int TotalTopicCount, int TotalCommentCount)>> LoadEntireIndexPageData();
    }
}
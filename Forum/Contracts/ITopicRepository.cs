using Forum.Models;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface ITopicRepository<Topic> : IDatabaseMainAccess<Topic> where Topic : class
    {
        IEnumerable<Topic> EachTopicRowOfCategoryID(IEnumerable<Category> categories);
        IEnumerable<Models.Topic> TopicBoardLoader(int categoryID);
        Dictionary<int, int> TotalNumberOfTopics(IEnumerable<Category> categories);
    }
}

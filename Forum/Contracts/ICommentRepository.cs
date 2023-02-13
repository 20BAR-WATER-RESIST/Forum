using Forum.Models;

namespace Forum.Contracts
{
    public interface ICommentRepository<Comment> : IDatabaseMainAccess<Comment> where Comment : class
    {
        Dictionary<int, int> BoardTopicCommentCount(IEnumerable<Topic> topic);
        Dictionary<int, int> CategoryCommentCounter(IEnumerable<Topic> topic);
    }
}

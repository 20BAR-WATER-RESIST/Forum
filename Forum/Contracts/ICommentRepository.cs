using Forum.Models;

namespace Forum.Contracts
{
    public interface ICommentRepository<Comment> : IDatabaseMainAccess<Comment> where Comment : class
    {
        Dictionary<int, int> TotalNumberOfComments(IEnumerable<Topic> topic);
    }
}

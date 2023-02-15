using Forum.Models;

namespace Forum.Contracts
{
    public interface ICommentRepository<Comment> : IDatabaseMainAccess<Comment> where Comment : class
    {
    }
}

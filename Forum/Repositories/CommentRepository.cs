using Forum.Context;
using Forum.Contracts;
using Forum.Models;

namespace Forum.Repositories
{
    public class CommentRepository : DatabaseMainAccess<Comment>, ICommentRepository<Comment>
    {
        public CommentRepository(DefaultDbContext context) : base(context)
        {
        }

        
    }
}

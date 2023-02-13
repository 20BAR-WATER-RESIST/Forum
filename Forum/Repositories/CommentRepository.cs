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

        public Dictionary<int, int> CategoryCommentCounter(IEnumerable<Topic> topic)
        {
            var groupedCommentsByCatId = from top in topic
                                         from com in _context.Comments
                                         where top.TopicID == com.TopicID
                                         group com by top.CategoryID into g
                                         select new { key = g.Key, total = g.Count() };

            var totalComments = new Dictionary<int, int>();

            foreach (var item in groupedCommentsByCatId)
            {
                totalComments.Add(item.key, item.total);
            }

            return totalComments;
        }

        public Dictionary<int, int> BoardTopicCommentCount(IEnumerable<Topic> topic)
        {
            var groupedCommentsByCatId = from top in topic
                                         from com in _context.Comments
                                         where top.TopicID == com.TopicID
                                         group com by top.TopicID into g
                                         select new { key = g.Key, total = g.Count() };

            var totalComments = new Dictionary<int, int>();

            foreach (var item in groupedCommentsByCatId)
            {
                totalComments.Add(item.key, item.total);
            }

            return totalComments;
        }
    }
}

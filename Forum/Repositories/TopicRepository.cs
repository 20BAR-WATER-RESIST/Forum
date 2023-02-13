using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Forum.Repositories
{
    public class TopicRepository : DatabaseMainAccess<Topic>, ITopicRepository<Topic>
    {

        public TopicRepository(DefaultDbContext context) : base(context)
        {
        }

        public IEnumerable<Topic> EachTopicRowOfCategoryID(IEnumerable<Category> categories)
        {
            var topics = from c in categories
                         from t in _context.Topics
                         where c.CategoryID == t.CategoryID && t.IsActive == true && c.IsActive == true
                         select t;

            return topics.OrderByDescending(t=>t.TopicAddedDate).DistinctBy(c=>c.CategoryID);
        }

        public Dictionary<int,int> TotalNumberOfTopics(IEnumerable<Category> categories)
        {
            var groupedTopicsByCatId = from cat in categories
                                      from top in _context.Topics
                                      where top.CategoryID == cat.CategoryID
                                      group top by cat.CategoryID into g
                                      select new { key = g.Key, total = g.Count() };

            var totalTopics = new Dictionary<int, int>();

            foreach(var item in groupedTopicsByCatId)
            {
                totalTopics.Add(item.key, item.total);
            }

            return totalTopics;
        }

        public IEnumerable<Topic> TopicBoardLoader(int categoryID)
        {
            IEnumerable<Topic> topics = from top in _context.Topics
                                        where top.CategoryID == categoryID && top.IsActive == true
                                        select top;

            return topics.OrderByDescending(t => t.TopicAddedDate).Take(5);
        }


    }
}

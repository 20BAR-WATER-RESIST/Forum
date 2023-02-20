using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Forum.Repositories
{
    public class TopicRepository : ITopicRepository
    {

        public TopicRepository(DefaultDbContext context)
        {
        }

    }
}

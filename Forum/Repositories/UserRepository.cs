using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Forum.Repositories
{
    public class UserRepository : DatabaseMainAccess<User>, IUserRepository<User>
    {
        public UserRepository(DefaultDbContext context) : base(context)
        {
        }

        public Dictionary<int,string> NewestTopicsAuthors(IEnumerable<Topic> lastTopics)
        {
            var author = from topic in lastTopics
                         from user in _context.Users
                         where topic.UserID == user.UserID
                         select new { key = topic.TopicID, name = user.UserName};

            var authorsDictionary = new Dictionary<int, string>();

            foreach (var item in author)
            {
                authorsDictionary.Add(item.key, item.name);
            }

            return authorsDictionary;
        }
    }
}

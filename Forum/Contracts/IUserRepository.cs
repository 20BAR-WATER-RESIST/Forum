using Forum.Models;

namespace Forum.Contracts
{
    public interface IUserRepository<User> : IDatabaseMainAccess<User> where User : class
    {
        Dictionary<int, string> NewestTopicsAuthors(IEnumerable<Topic> lastTopics);
    }
}

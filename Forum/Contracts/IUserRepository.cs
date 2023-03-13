using Forum.Models;

namespace Forum.Contracts
{
    public interface IUserRepository
    {
        Task<User> LoadUserProfileHeader(string profileName);
    }
}

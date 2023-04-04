using Forum.Models;

namespace Forum.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserByName(string profileName);
        Task<User> LoadUserProfileHeader(string profileName);
    }
}

using Forum.Models;
using Forum.Models.ReportSystem;

namespace Forum.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserByName(string profileName);
        Task<User> LoadUserProfileHeader(string profileName);
        Task<List<ReportCategory>> LoadUsersReportCategories();
    }
}

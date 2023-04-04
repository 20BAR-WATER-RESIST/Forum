using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.Contracts
{
    public interface ILoginRepository
    {
        Task<User> GetUserAccData(string email, string password);
        Task<string> GetVerification(string email, string password);
    }
}

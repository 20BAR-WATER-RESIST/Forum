using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.Contracts
{
    public interface ILoginRepository
    {
        Task<User> GetUserAccData(string email, string password);
        Task<bool> GetVerification(string email, string password);
    }
}

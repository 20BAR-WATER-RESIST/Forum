using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.Contracts
{
    public interface ILoginRepository
    {
        Task<(string UserEmail, string UserName, string UserTypeName)> GetUserAccData(string email, string password);
        Task<bool> GetVerification(string email, string password);
    }
}

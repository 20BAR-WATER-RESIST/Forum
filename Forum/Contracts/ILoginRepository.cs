using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.Contracts
{
    public interface ILoginRepository
    {
        Task<User> GetUserEmail(EmailAddressAttribute email);
        Task<User> GetUserPassword(EmailAddressAttribute email, string password);
    }
}

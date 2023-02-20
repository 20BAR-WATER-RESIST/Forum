using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Reflection.Metadata.Ecma335;

namespace Forum.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(DefaultDbContext context)
        {
        }


    }
}

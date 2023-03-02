using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Forum.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DefaultDbContext _context;

        public LoginRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<bool> GetVerification(string email, string password)
        {
            using(var connection = _context.CreateConnection())
            {
                var querry = @"select UserEmail, UserPassword from users u
                               where u.UserEmail = @Email and u.UserPassword = @Password;";

                var results = await connection.QueryFirstOrDefaultAsync<(string UserEmail, string UserPassword)>(querry, new { Email = email, Password = password });

                if(results.UserEmail == email && results.UserPassword == password)
                {
                    return true;
                }
                else { return false; }
            }
        }

        public async Task<(string UserEmail, string UserName, string UserTypeName)> GetUserAccData(string email, string password)
        {
            using (var conntection = _context.CreateConnection())
            {
                var querry = @"select UserEmail, UserName, ut.UserTypeName from users u
                               left join userstypes ut on u.UserTypeID = ut.UserTypeID 
                               where u.UserEmail = @Email AND u.UserPassword = @Password;";

                var results = await conntection.QueryFirstOrDefaultAsync<(string UserEmail, string UserName, string UserTypeName)>(querry, new { Email = email, Password = password });
                return results;
            }
        }
    }
}

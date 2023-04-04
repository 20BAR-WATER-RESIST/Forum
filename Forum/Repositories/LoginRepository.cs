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

        public async Task<string> GetVerification(string email, string password)
        {
            using(var connection = _context.CreateConnection())
            {
                var query = @"select UserEmail, UserPassword from users u
                               where u.UserEmail = @Email and u.UserPassword = @Password;";

                var results = await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email, Password = password });

                if(results != null && results.UserEmail == email && results.UserPassword == password)
                {
                    return string.Empty;
                }
                else { return "Błędny adres email lub hasło. Spróbuj ponownie."; }
            }
        }

        public async Task<User> GetUserAccData(string email, string password)
        {
            using (var conntection = _context.CreateConnection())
            {
                var query = @"select UserEmail, UserName, u.UserTypeID, ut.UserTypeName from users u
                               left join userstypes ut on u.UserTypeID = ut.UserTypeID 
                               where u.UserEmail = @Email AND u.UserPassword = @Password;";

                var results = await conntection.QueryAsync<User, UserType, User>(sql: query,
                    map: (user, usertype) =>
                    {
                        user.UserTypes = usertype;
                        return user;
                    },
                    param: new { @Email = email, Password = password},
                    splitOn: "UserTypeID"
                );
                return results.FirstOrDefault();
            }
        }
    }
}

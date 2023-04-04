using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;

namespace Forum.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly DefaultDbContext _context;

        public RegisterRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<string> CheckUsernameAndUseremailAvailability(string username, string email)
        {
            using(var connection = _context.CreateConnection())
            {
                var query = @"SELECT UserName FROM Users WHERE UserName = @Username;
                            SELECT UserEmail FROM Users WHERE UserEmail = @Useremail;";

                var multiQueryResult = await connection.QueryMultipleAsync(query, new { Username = username, Useremail = email });
                var userWithUserName = await multiQueryResult.ReadFirstOrDefaultAsync<string>();
                var userWithEmail = await multiQueryResult.ReadFirstOrDefaultAsync<string>();


                    if (string.IsNullOrEmpty(userWithUserName) && string.IsNullOrEmpty(userWithEmail))
                    {
                        // Nie znaleziono nazwy użytkownika ani adresu e-mail
                        return string.Empty;
                    }
                    else if (!string.IsNullOrEmpty(userWithUserName) && string.IsNullOrEmpty(userWithEmail))
                    {
                        // Nie znaleziono nazwy użytkownika
                        return "The provided username is already taken.";
                    }
                    else if (!string.IsNullOrEmpty(userWithEmail) && string.IsNullOrEmpty(userWithUserName))
                    {
                        // Nie znaleziono adresu e-mail
                        return "The provided email address is already taken.";
                    }
                    else
                    {
                        // Nazwa użytkownika i adres e-mail zostały znalezione
                        return "The provided username and email address are already taken.";
                    }
                
            }
        }

        public async Task<bool> CompleteRegister(string regEmail, string regUsername, string regPassword)
        {
            using(var connection = _context.CreateConnection())
            {
                var querry = @"insert into users (UserName,UserPassword,UserEmail,UserRegisteredDate,IsActive,UserTypeID)
                               values(@Username,@Password,@Email,@Date,'1','1');";

                var checkingQuerry = @"select UserName,UserPassword,UserEmail from users u
                                       where u.UserEmail = @Email and u.UserName = @Username and u.UserPassword = @Password ";

                await connection.QueryAsync(querry, new { Email = regEmail, Username = regUsername, Password = regPassword, Date = DateTime.UtcNow });

                var checkResults = await connection.QueryFirstOrDefaultAsync<(string Username, string Pass, string Mail)>(checkingQuerry, new { Email = regEmail, Username = regUsername, Password = regPassword });

                if(checkResults.Username == regUsername && checkResults.Mail == regEmail && checkResults.Pass == regPassword)
                {
                    return true;
                } else { return false; }
            }
        }
    }
}

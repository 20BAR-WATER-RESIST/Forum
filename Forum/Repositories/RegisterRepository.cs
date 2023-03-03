using Dapper;
using Forum.Context;
using Forum.Contracts;

namespace Forum.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly DefaultDbContext _context;

        public RegisterRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUsernameAvailability(string username)
        {
            using(var connection = _context.CreateConnection())
            {
                var querry = @"select UserName as RegisterUsername from users u
                               where u.UserName = @Username;";

                var results = await connection.QueryFirstOrDefaultAsync<string>(querry, new { Username = username });

                if (results == null)
                {
                    return true;
                } else { return false; }
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

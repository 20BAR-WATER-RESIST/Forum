using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Models.ReportSystem;
using System.Reflection.Metadata.Ecma335;

namespace Forum.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DefaultDbContext _context;

        public UserRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<User> LoadUserProfileHeader(string profileName)
        {
            using(var connection = _context.CreateConnection())
            {
                var query = @"select u.UserID, u.UserName, u.UserRegisteredDate, u.UserTypeID, ut.UserTypeName from users u
                              left join userstypes ut on ut.UserTypeID = u.UserTypeID
                              where u.UserName = @Name;";
                var result = await connection.QueryAsync<User, UserType, User>(
                    sql: query,
                    map: (user, userType) =>
                    {
                        user.UserTypes = userType;
                        return user;
                    },
                    param: new { Name = profileName },
                    splitOn: "UserTypeID"
                    );
                return result.FirstOrDefault();
            }
        }

        public async Task<User> FindUserByName(string profileName)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select u.UserID, u.UserName, u.UserRegisteredDate, u.UserTypeID from users u
                              where u.UserName = @Name;";
                var result = await connection.QueryAsync<User>(query, new {Name = profileName});

                return result.FirstOrDefault();

            }
        }

        public async Task<List<ReportCategory>> LoadUsersReportCategories()
        {
            using (var connection = _context.CreateConnection())
            {

                var query = @"select * from reportcategory rc
                              where rc.ReportCategoryTarget = 'Users' and rc.IsActive = true;";

                var results = await connection.QueryAsync<ReportCategory>(query);

                return results.ToList();
            }
        }
    }
}

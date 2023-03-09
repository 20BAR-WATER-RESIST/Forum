using Dapper;
using Forum.Context;
using Forum.Contracts;
using System.Runtime.CompilerServices;

namespace Forum.Repositories
{
    public class CRUD_Repository : ICRUD_Repository
    {
        private readonly DefaultDbContext _context;

        public CRUD_Repository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task InsertComment(string text, string username, int id)
        {
            using(var connection = _context.CreateConnection())
            {
                var querryGetUserID = @"select UserID from users u
                                        where u.UserName = @UserName;";
                var resultID = await connection.QueryFirstOrDefaultAsync<int>(querryGetUserID, new {UserName = username});

                var querryInsert = @"INSERT INTO comments (CommentText, CommentAddedTime, IsActive, UserID, TopicID)
                                     VALUES (@Text, @Date, 1, @ID, @TopID);";
                await connection.QueryAsync(querryInsert, new {Text = text, Date = DateTime.Now, ID = resultID, TopID = id});
            }
        }
    }
}

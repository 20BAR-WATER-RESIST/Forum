using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Globalization;

namespace Forum.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DefaultDbContext _context;

        public CommentRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCommentAmmountPerTopic(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"select count(CommentID) from comments com
                               where com.TopicID = @SiteID;";

                var results = await connection.QueryFirstOrDefaultAsync<int>(querry, new { SiteID = id });
                return results;
            }
        }

        public async Task<List<(string CommentText, DateTime CommentAddedTime, bool IsActive, string UserName)>> LoadPlotsComments(int id, int currentPage)
        {
            using(var connection = _context.CreateConnection())
            {
                int pageCalc;
                if (currentPage <= 1)
                {
                    pageCalc = 0;
                }
                else { pageCalc = (currentPage - 1) * 10; }

                var querry = @"select CommentText, CommentAddedTime, com.IsActive, u.UserName AS CommentAuthor from comments com
                                left join users u on u.UserID = com.UserID
                                where com.TopicID = @SiteID
                                order by com.CommentAddedTime ASC
                                limit 10 offset @PageNumber;";

                var results = (await connection.QueryAsync<(string, DateTime,bool,string)>(querry, new {SiteID=id, PageNumber = pageCalc})).ToList();
                return results;
            }
        }
        
    }
}

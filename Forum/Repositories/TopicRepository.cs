using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Forum.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DefaultDbContext _context;

        public TopicRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTopicAmmountPerCategory(int id)
        {
            using(var connection = _context.CreateConnection())
            {
                var querry = @"select count(TopicID) from topics t
                               where t.CategoryID = @CategoryID AND t.IsActive = true;";

                var results = await connection.QueryFirstOrDefaultAsync<int>(querry, new {CategoryID = id});
                return results;
            }
        }

        public async Task<List<(int CategoryID,int TopicID,string TopicName,string TopicAuthor, DateTime TopicAddedDate, int CommentCount,string CommentAuthor,DateTime CommentAddedTime)>> LoadBoard(int id, int currentPage)
        {
            using(var connection = _context.CreateConnection())
            {
                int pageCalc;
                if(currentPage <= 1)
                {
                    pageCalc = 0;
                }
                else { pageCalc = (currentPage-1) * 10; }

                var querry = @"select CategoryID, TopicID, TopicName, TopicAuthor, TopicAddedDate, CommentCount AS TotalCommentCount, LastCommentAuthor AS CommentAuthor, CommentAddedTime from 
                                (
	                                select t.CategoryID, t.TopicID, t.TopicName, t.TopicAddedDate, com.CommentAddedTime,
                                  (select u2.UserName from users u2 where t.UserID = u2.UserID) AS TopicAuthor,
                                  (select count(distinct com2.CommentID) from comments com2 where t.TopicID = com2.TopicID AND t.CategoryID = @SiteID AND com2.IsActive = true) AS CommentCount,
                                  (select u3.UserName from users u3 where u3.UserID = com.UserID) AS LastCommentAuthor,
                                  row_number() over(partition by t.TopicID order by com.CommentAddedTime DESC) as rownumb
                                  from topics t
                                  left join comments com on t.TopicID = com.TopicID
                                  left join users u on u.UserID = com.UserID
                                  where t.CategoryID = @SiteID AND t.IsActive = true
                                  order by t.TopicAddedDate DESC
                                ) as temp
                                where rownumb = 1
                                limit 10 offset @PageNumber;";

            var results = (await connection.QueryAsync<(int,int,string,string,DateTime,int,string,DateTime)>(querry, new {SiteID = id, PageNumber = pageCalc })).ToList();
            return results;
            }
        }

        public async Task<List<(string TopicName, string TopicDescription, DateTime TopicAddedDate, string TopicAuthor)>> LoadPlotTopic(int id)
        {
            using(var connection = _context.CreateConnection())
            {
                var querry = @"select TopicName, TopicDescription, TopicAddedDate, 
                            (select u.UserName from users u where u.UserID = t.UserID) AS TopicAuthor
                            from topics t
                            where t.TopicID = @PlotID;";

                var result = (await connection.QueryAsync<(string,string,DateTime,string)>(querry, new {PlotID = id })).ToList();
                return result;
            }
        }

        /* New Layout */

        public async Task<List<(int TopicID, string TopicName, string TopicDescription, int ViewCount, int CommentCount, TimeSpan TimeDiff)>> IndexPageBoardTopics()
        {
            using(var connection = _context.CreateConnection())
            {
                var querry = @"select TopicID, TopicName, TopicDescription, ViewCount,
                               (select count(CommentID) from comments com where com.TopicID = t.TopicID) AS CommentCount,
                               (select time(timediff(now(), t.TopicAddedDate)) from topics t2 where t2.TopicID = t.TopicID) as TimeDiff 
                               from topics t
                               where t.IsActive = true
                               order by t.TopicAddedDate desc
                               limit 10;";

                var results = (await connection.QueryAsync<(int, string, string, int, int, TimeSpan)>(querry)).ToList();
                return results;
            }
        }

        public async Task<List<(int CategoryID, string CategoryName, int TotalTopicCount)>> IndexPageBoardCategories()
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"select CategoryID, CategoryName, 
                               (select count(TopicID) from topics t where t.CategoryID = c.CategoryID and t.IsActive = true) as TotalTopicCount
                               from categories c
                               where c.IsActive = true;";
                var results = (await connection.QueryAsync<(int, string, int)>(querry)).ToList();
                return results;
            }
        }

        public async Task<List<(int TopicID, string TopicName, DateTime TopicAddedDate, int CommentCount)>> LatestHotTopics()
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"select TopicID, TopicName, TopicAddedDate,
                               (select count(CommentID) from comments com where com.TopicID = topics.TopicID) as CommentCount
                               from topics
                               where YEARWEEK(TopicAddedDate) = YEARWEEK(CURDATE()) and topics.IsActive = true
                               union
                               select TopicID, TopicName, TopicAddedDate,
                               (select count(CommentID) from comments com where com.TopicID = topics.TopicID) as CommentCount
                               from topics
                               where topics.IsActive = true
                               order by TopicAddedDate desc, CommentCount desc
                               limit 10;";
                var results = (await connection.QueryAsync<(int, string, DateTime, int)>(querry)).ToList();
                return results;
            }
        }

    }
}

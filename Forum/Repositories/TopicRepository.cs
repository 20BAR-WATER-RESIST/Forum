using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Models.ReportSystem;
using MySqlX.XDevAPI.Common;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace Forum.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DefaultDbContext _context;

        public TopicRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>> LoadIndexPageTopics()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select t.TopicID, t.TopicName, t.TopicDescription, t.ViewCount, count(com.CommentID) as TotalCommentCount, time(timediff(now(), t.TopicAddedDate)) as TimeDiff, t.UserID, u.UserName
                              from topics t
                              left join comments com on com.TopicID = t.TopicID
                              left join users u on u.UserID = t.UserID
                              where t.IsActive = true
                              group by t.TopicID
                              order by t.TopicAddedDate desc
                              limit 10;";

                var results = await connection.QueryAsync<Topic, User, Topic>(sql: query,
                    map: (topic, user) =>
                    {
                        topic.Users = user;
                        return topic;
                    },
                    splitOn: "UserID"
                );
                return results.ToList();
            }
        }

        public async Task<List<Topic>> LoadBoardPageTopics(int id, int currentPage)
        {
            using (var connection = _context.CreateConnection())
            {
                int pageCalc;
                if (currentPage <= 1)
                {
                    pageCalc = 0;
                }
                else { pageCalc = (currentPage - 1) * 10; }

                var query = @"select t.TopicID, t.TopicName, t.TopicDescription, t.ViewCount, count(com.CommentID) as TotalCommentCount, time(timediff(now(), t.TopicAddedDate)) as TimeDiff, t.UserID, u.UserName
                              from topics t
                              left join comments com on com.TopicID = t.TopicID
                              left join users u on u.UserID = t.UserID
                              where t.IsActive = true and t.CategoryID = @ID
                              group by t.TopicID
                              order by t.TopicAddedDate desc
                              limit 10 offset @PageNumber;";

                var results = await connection.QueryAsync<Topic, User, Topic>(sql: query,
                    map: (topic, user) =>
                    {
                        topic.Users = user;
                        return topic;
                    },
                    param: new { ID = id, PageNumber = pageCalc },
                    splitOn: "UserID"
                );
                return results.ToList();
            }
        }

        public async Task<List<Topic>> LatestHotTopics()
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"select TopicID, TopicName, TopicAddedDate,
                               (select count(CommentID) from comments com where com.TopicID = topics.TopicID) as TotalCommentCount
                               from topics
                               where YEARWEEK(TopicAddedDate) = YEARWEEK(CURDATE()) and topics.IsActive = true
                               union
                               select TopicID, TopicName, TopicAddedDate,
                               (select count(CommentID) from comments com where com.TopicID = topics.TopicID) as TotalCommentCount
                               from topics
                               where topics.IsActive = true
                               order by TopicAddedDate desc, TotalCommentCount desc
                               limit 10;";
                var results = await connection.QueryAsync<Topic>(querry);
                return results.ToList();
            }
        }

        public async Task<List<Topic>> BoardLatestHotTopics(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"select TopicID, TopicName, TopicAddedDate,
                               (select count(CommentID) from comments com where com.TopicID = topics.TopicID) as TotalCommentCount
                               from topics
                               where YEARWEEK(TopicAddedDate) = YEARWEEK(CURDATE()) and topics.IsActive = true and topics.CategoryID = @ID
                               union
                               select TopicID, TopicName, TopicAddedDate,
                               (select count(CommentID) from comments com where com.TopicID = topics.TopicID) as TotalCommentCount
                               from topics
                               where topics.IsActive = true and topics.CategoryID = @ID
                               order by TopicAddedDate desc, TotalCommentCount desc
                               limit 10;";
                var results = await connection.QueryAsync<Topic>(sql: querry, param: new { ID = id });
                return results.ToList();
            }
        }

        public async Task<List<Topic>> LoadPlot(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"select TopicID, TopicName, TopicDescription, TopicAddedDate, VotePlus, VoteMinus, u.UserID, u.UserName, 
                                 ut.UserTypeID, ut.UserTypeName
                                 from topics t
                                 left join users u on u.UserID = t.UserID
                                 left join userstypes ut on u.UserTypeID = ut.UserTypeID
                                 where t.TopicID = @ID;";

                var result = await connection.QueryAsync<Topic, User, UserType, Topic>(
                    sql: query,
                    map: (topic, user, userType) =>
                    {
                        topic.Users = user;
                        user.UserTypes = userType;
                        return topic;
                    },
                    param: new { ID = id },
                    splitOn: "UserID,UserTypeID"
                );

                return result.ToList();

            }
        }

        public async Task<List<Topic>> LoadUserProfileTopics(string name)
        {
            using(var connection = _context.CreateConnection())
            {
                var query = @"select t.TopicID, t.TopicName, t.TopicDescription, t.TopicAddedDate from topics t
                              left join users u on t.UserID = u.UserID
                              where u.UserName = @Name
                              order by t.TopicAddedDate desc;";
                var results = await connection.QueryAsync<Topic>(
                    sql: query,
                    param: new { Name = name }
                );

                return results.ToList();
            }
        }

        public async Task<List<Topic>> SearchTopics(string text)
        {
            using(var connection = _context.CreateConnection())
            {
                var searchString = $"%{text}%";

                var query = @"select t.TopicID, t.TopicName, t.TopicDescription, t.ViewCount, count(com.CommentID) as TotalCommentCount, time(timediff(now(), t.TopicAddedDate)) as TimeDiff, t.UserID, u.UserName
                              from topics t
                              left join comments com on com.TopicID = t.TopicID
                              left join users u on u.UserID = t.UserID
                              where t.IsActive = true and t.TopicName like @Text or t.IsActive = true and t.TopicDescription like @Text
                              group by t.TopicID
                              order by t.TopicAddedDate desc
                              limit 10;";

                var results = await connection.QueryAsync<Topic, User, Topic>(sql: query,
                    map: (topic, user) =>
                    {
                        topic.Users = user;
                        return topic;
                    },
                    param: new { Text = searchString },
                    splitOn: "UserID"
                );
                return results.ToList();
            }
        }

        public async Task<List<ReportCategory>> LoadTopicReportCategories()
        {
            using (var connection = _context.CreateConnection())
            {

                var query = @"select * from reportcategory rc
                              where rc.ReportCategoryTarget = 'Topic' and rc.IsActive = true;";

                var results = await connection.QueryAsync<ReportCategory>(query);

                return results.ToList();
            }
        }
    }
}

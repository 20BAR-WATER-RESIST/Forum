using Dapper;
using Forum.Context;
using Forum.Contracts.Manager;
using Forum.Models;
using Forum.Models.ReportSystem;

namespace Forum.Repositories.Manager
{
    public class AdministrationDashboardRepository : IAdministrationDashboardRepository
    {
        private readonly DefaultDbContext _context;

        public AdministrationDashboardRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReportBase>> LoadReports()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"SELECT rb.ReportID, rb.ReportType, rb.ReportCategoryID, rb.ReportAuthor, rb.ReportMessage, rb.ReportAddedDate, rb.ReportTargetID, rb.IsActive,
                                   rc.ReportCategoryName,
                                   u.UserID, u.UserName, tu.IsActive AS TargetUserAccountStatus,
                                   t.IsActive AS TargetTopicAccountStatus,
                                   c.IsActive AS TargetCommentAccountStatus,
                                   CASE
                                       WHEN rb.ReportType = 'Users' THEN tu.UserName
                                       ELSE NULL
                                   END as ReportTargetUsername,
                                   CASE
                                       WHEN rb.ReportType = 'Comment' THEN c.CommentText
                                       ELSE NULL
                                   END as ReportTargetCommentText,
                                   CASE
                                       WHEN rb.ReportType = 'Comment' THEN c.CommentAddedTime
                                       ELSE NULL
                                   END as CommentAddedTime,
                                   CASE
                                       WHEN rb.ReportType = 'Comment' THEN u2.UserName
                                       ELSE NULL
                                   END as CommentAuthor
                            FROM reportbase rb
                            LEFT JOIN reportcategory rc ON rb.ReportCategoryID = rc.ReportCategoryID AND rc.IsActive = true
                            LEFT JOIN topics t on rb.ReportTargetID = t.TopicID AND rb.ReportType = 'Topic'
                            LEFT JOIN users u ON rb.ReportAuthor = u.UserID
                            LEFT JOIN users tu ON rb.ReportTargetID = tu.UserID AND rb.ReportType = 'Users'
                            LEFT JOIN comments c ON rb.ReportTargetID = c.CommentID AND rb.ReportType = 'Comment'
                            LEFT JOIN users u2 ON c.UserID = u2.UserID
                            WHERE rb.IsActive = true
                            ORDER BY ReportAddedDate DESC;";

                var result = await connection.QueryAsync<dynamic>(query);

                return result.Select(r => new ReportBase
                {
                    ReportID = r.ReportID,
                    ReportType = r.ReportType,
                    ReportCategoryID = r.ReportCategoryID,
                    ReportAuthor = r.ReportAuthor,
                    ReportMessage = r.ReportMessage,
                    ReportAddedDate = r.ReportAddedDate,
                    ReportTargetID = r.ReportTargetID,
                    IsActive = r.IsActive,
                    TargetUserAccountStatus = r.TargetUserAccountStatus,
                    TargetTopicAccountStatus = r.TargetTopicAccountStatus,
                    TargetCommentAccountStatus = r.TargetCommentAccountStatus,
                    ReportCategory = new ReportCategory
                    {
                        ReportCategoryName = r.ReportCategoryName
                    },
                    User = new User
                    {
                        UserID = r.UserID,
                        UserName = r.UserName
                    },
                    ReportTargetUsername = r.ReportTargetUsername,
                    Comment = new Comment
                    {
                        CommentText = r.ReportTargetCommentText,
                        CommentAuthor = r.CommentAuthor,
                    },
                    ReportBaseReportedCommentAddedTime = r.CommentAddedTime,
                }).ToList();
            }
        }

        public async Task<List<User>> LoadBansEndingSoon()
        {
            using(var connection = _context.CreateConnection())
            {
                var query = @"SELECT u.UserID, u.UserName, u.UserBannedTime, u.IsActive, u.UserBanReason FROM users u
                              WHERE UserBannedTime > NOW() AND UserBannedTime <= DATE_ADD(NOW(), INTERVAL 7 DAY) AND IsActive = false;";

                var result = await connection.QueryAsync<User>(query);

                return result.ToList();
            }
        }

        public async Task DeleteReport(int reportId)
        {
            using(var connection = _context.CreateConnection())
            {
                var query = "DELETE FROM reportbase WHERE ReportID = @ID;";

                await connection.ExecuteAsync(query, param: new { ID = reportId });
            }
        }

        public async Task<string> BanReportTargetAuthor(int reportId, string reason, DateTime dateTime)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"UPDATE users
                              SET UserBannedTime = @DATETIME, IsActive = 0, UserBanReason = @REASON
                              WHERE UserID = @ID;";

                await connection.ExecuteAsync(query, param: new { ID = reportId, REASON = reason, DATETIME = dateTime });

                return "Operacja przebiegła pomyślnie";
            }
        }

        public async Task<string> QuickBanMethod(string reportedType, int reportedID)
        {
            using (var connection = _context.CreateConnection())
            {
                string query;

                if(reportedType != null && reportedType == "Topic") 
                {
                      query = @"UPDATE topics
                              SET IsActive = 0, TopicBannedDate = NOW()
                              WHERE TopicID = @REPORTEDID;";
                } else if(reportedType != null && reportedType == "Comment")
                {
                      query = @"UPDATE comments
                              SET IsActive = 0, CommentBannedTime = NOW()
                              WHERE CommentID = @REPORTEDID;";
                } else
                {
                    return "Brak zadeklarowanego typu";
                }

                await connection.ExecuteAsync(query, new { REPORTTYPE = reportedType, REPORTEDID = reportedID });

                return "Operacja przebiegła pomyślnie";
            }
        }

        public async Task<Comment> loadReportedCommentData(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"SELECT c.CommentText, c.CommentAddedTime, u.UserName AS CommentAuthor from comments c
                               LEFT JOIN users u ON c.UserID = u.UserID
                               WHERE c.CommentID = @ID;";

                var comment = await connection.QueryAsync<Comment>(querry, new { ID = id });


                return comment.FirstOrDefault();
            }
        }

        public async Task LiftBanMethod(int userID)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"UPDATE users
                              SET UserBannedTime = null, IsActive = 1, UserBanReason = null
                              WHERE UserID = @ID;";

                await connection.ExecuteAsync(query, new { ID = userID });

            }
        }
    }
}

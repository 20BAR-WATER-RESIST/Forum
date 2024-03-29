﻿using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Models.ReportSystem;
using MySql.Data.MySqlClient;
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

        public async Task<List<Comment>> LoadPlotComments(int topicId, int currentPage)
        {
            using (var connection = _context.CreateConnection())
            {

                int pageCalc;
                if (currentPage <= 1)
                {
                    pageCalc = 0;
                }
                else { pageCalc = (currentPage - 1) * 10; }

                var query = @"SELECT c.CommentID, c.CommentText, c.CommentAddedTime, c.UserID, c.VotePlus, c.VoteMinus, c.IsActive, u.UserName, u.UserTypeID,
                              ut.UserTypeName
                              FROM comments c
                              INNER JOIN users u ON c.UserID = u.UserID
                              INNER JOIN userstypes ut ON u.UserTypeID = ut.UserTypeID
                              WHERE c.TopicID = @TopicId
                              order by c.CommentAddedTime asc
                              limit 10 offset @PageNumber;";


                var result = await connection.QueryAsync<Comment, User, UserType, Comment>(
                    query,
                    map: (comment, user, userType) =>
                    {
                         comment.User = user;
                         comment.User.UserTypes = userType;
                         return comment;
                    },
                    param: new { TopicId = topicId, PageNumber = pageCalc },
                    splitOn: "UserName,UserTypeName");

                return result.ToList();
            }
        }

        public async Task<List<Comment>> LoadUserProfileComments(string name)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select c.CommentID, c.CommentText, c.CommentAddedTime, t.TopicID from comments c
                              left join users u on c.UserID = u.UserID
                              left join topics t on t.TopicID = c.TopicID
                              where u.UserName = @Name
                              order by c.CommentAddedTime desc;";
                var results = await connection.QueryAsync<Comment>(
                    sql: query,
                    param: new { Name = name }
                );

                return results.ToList();
            }
        }

        public async Task<List<ReportCategory>> LoadCommentsReportCategories()
        {
            using (var connection = _context.CreateConnection())
            {

                var query = @"select * from reportcategory rc
                              where rc.ReportCategoryTarget = 'Comment' and rc.IsActive = true;";

                var results = await connection.QueryAsync<ReportCategory>(query);

                return results.ToList();
            }
        }

    }
}

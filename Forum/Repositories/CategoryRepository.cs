using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;

namespace Forum.Repositories
{
    public class CategoryRepository :  ICategoryRepository
    {
        private readonly DefaultDbContext _context;

        public CategoryRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<List<(int CategoryID, string CategoryName, string CategoryDescription, string TopicName, string UserName, DateTime TopicAddedDate, int TotalTopicCount, int TotalCommentCount)>> LoadEntireIndexPageData()
        {
            using (var connection = _context.CreateConnection())
            {
                var query =
                   @"SELECT CategoryID, CategoryName, CategoryDescription, TopicName, UserName, TopicAddedDate, TotalTopicCount, TotalCommentCount
                    FROM (
                        SELECT c.CategoryID, c.CategoryName, c.CategoryDescription, t.TopicName, u.UserName, t.TopicAddedDate,
                               (SELECT COUNT(DISTINCT t2.TopicID) FROM topics t2 WHERE t2.CategoryID = c.CategoryID AND t2.IsActive = true) AS TotalTopicCount,
                               (SELECT COUNT(DISTINCT com.CommentID) FROM comments com JOIN topics t2 ON com.TopicID = t2.TopicID WHERE t2.CategoryID = c.CategoryID AND com.IsActive = true AND t2.IsActive = true) AS TotalCommentCount,
                               ROW_NUMBER() OVER (PARTITION BY c.CategoryID ORDER BY t.TopicAddedDate DESC) AS RowNum
                        FROM Categories c
                        LEFT JOIN topics t ON c.CategoryID = t.CategoryID
                        LEFT JOIN users u ON t.UserID = u.UserID
                        WHERE t.IsActive = true
                    ) temp
                    WHERE RowNum = 1;";
                var results = (await connection.QueryAsync<(int, string, string, string, string, DateTime, int, int)>(query)).ToList();
                return results;
            }
        }

    }
}

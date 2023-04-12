using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Forum.Models.ReportSystem;

namespace Forum.Repositories
{
    public class CategoryRepository :  ICategoryRepository
    {
        private readonly DefaultDbContext _context;

        public CategoryRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetTopicAmmountPerCategory(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var querry = @"select count(TopicID) as TotalTopicCount from topics t
                               where t.CategoryID = @CategoryID AND t.IsActive = true;";

                var results = await connection.QueryFirstOrDefaultAsync<Category>(querry, new { CategoryID = id });
                return results;
            }
        }

        public async Task<List<Category>> LoadListOfCategories()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select cat.CategoryID, cat.CategoryName from categories cat
                              where cat.IsActive = true
                              order by cat.CategoryID;";
                var results = await connection.QueryAsync<Category>(query);

                return results.ToList();
            }
        }

        public async Task<List<Category>> LoadIndexPageCategories()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"select c.CategoryID, c.CategoryName, count(t.TopicID) as TotalTopicCount
                              from categories c
                              left join topics t on c.CategoryID = t.CategoryID and t.IsActive = true
                              where c.IsActive = true
                              group by c.CategoryID;";
                var results = await connection.QueryAsync<Category>(query);

                return results.ToList();
            }
        }
    }
}

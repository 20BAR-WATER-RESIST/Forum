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
    }
}

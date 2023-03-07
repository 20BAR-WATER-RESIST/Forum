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

    }
}

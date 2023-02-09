using Forum.Context;
using Forum.Contracts;
using Forum.Models;

namespace Forum.Repositories
{
    public class CategoryRepository : DatabaseMainAccess<Category>, ICategoryRepository<Category>
    {
        private readonly DefaultDbContext context;

        public CategoryRepository(DefaultDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}

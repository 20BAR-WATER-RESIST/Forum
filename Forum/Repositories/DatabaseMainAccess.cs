using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Forum.Repositories
{
    public class DatabaseMainAccess<TEntity> : IDatabaseMainAccess<TEntity> where TEntity : class
    {
        protected readonly DefaultDbContext _context;

        public DatabaseMainAccess(DefaultDbContext context)
        {
            _context = context;
        }

    }
}

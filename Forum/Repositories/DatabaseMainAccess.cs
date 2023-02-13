using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>()
                .ToList();
        }

        public virtual TEntity Find(int id)
        {
            return _context.Set<TEntity>()
                .Find(id);
        }

        public virtual IEnumerable<TEntity> FindRange(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).ToList();
        }
        

        //Zastanów się czy to zostawić...
        public virtual TEntity FindFirstOrDefault(int id) => null;
    }
}

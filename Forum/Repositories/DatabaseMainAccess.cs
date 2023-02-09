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

        private protected IEnumerable<TEntity> allCategories { get; private set; }
        private protected IEnumerable<TEntity> allTopics { get; private set; }
        private protected IEnumerable<TEntity> allComments { get; private set; }

        public IEnumerable<TEntity> LoadAllCategories()
        {
            return allCategories = _context.Set<TEntity>()
                 .ToList();
        }

        public IEnumerable<TEntity> LoadAllTopics()
        {
           return allTopics = _context.Set<TEntity>()
                .ToList();
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

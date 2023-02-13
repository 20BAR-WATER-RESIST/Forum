using Forum.Models;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface IDatabaseMainAccess<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(int id);
        TEntity FindFirstOrDefault(int id);
        IEnumerable<TEntity> FindRange(Expression<Func<TEntity, bool>> expression);
    }
}

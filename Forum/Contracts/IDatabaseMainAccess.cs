using Forum.Models;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface IDatabaseMainAccess<TEntity> where TEntity : class
    {
    }
}

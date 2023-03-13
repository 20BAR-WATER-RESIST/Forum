using Forum.Models;

namespace Forum.Contracts
{
    public interface ICategoryRepository
    {
        Task<List<Category>> LoadListOfCategories();
    }
}
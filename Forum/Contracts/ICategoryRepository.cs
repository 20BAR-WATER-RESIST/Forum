using Forum.Models;

namespace Forum.Contracts
{
    public interface ICategoryRepository
    {
        Task<List<Category>> LoadListOfCategories();
        Task<List<Category>> LoadIndexPageCategories();
        Task<Category> GetTopicAmmountPerCategory(int id);
    }
}
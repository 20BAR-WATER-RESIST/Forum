namespace Forum.Contracts
{
    public interface ICategoryRepository<Category> : IDatabaseMainAccess<Category> where Category : class
    {

    }
}
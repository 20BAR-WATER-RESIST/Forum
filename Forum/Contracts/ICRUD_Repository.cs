namespace Forum.Contracts
{
    public interface ICRUD_Repository
    {
        Task InsertComment(string text, string username, int id);
    }
}

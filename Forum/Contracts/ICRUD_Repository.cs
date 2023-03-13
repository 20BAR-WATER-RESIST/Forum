namespace Forum.Contracts
{
    public interface ICRUD_Repository
    {
        Task InsertComment(string text, string username, int id);
        Task InsertTopic(int catID, string tName, string tDescription, string userName);
    }
}

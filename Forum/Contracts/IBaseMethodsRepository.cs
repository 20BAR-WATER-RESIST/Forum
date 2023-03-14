namespace Forum.Contracts
{
    public interface IBaseMethodsRepository
    {
        Task<string> TopicTimer(TimeSpan dataTime);
        Task<string> TrimString(string description, int lenght);
    }
}

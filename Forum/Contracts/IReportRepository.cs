namespace Forum.Contracts
{
    public interface IReportRepository
    {
        Task<string> SendReport(string repoAuth, int catID, string repMessage, int targID, string repoType);
    }
}

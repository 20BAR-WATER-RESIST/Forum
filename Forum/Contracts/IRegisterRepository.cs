namespace Forum.Contracts
{
    public interface IRegisterRepository
    {
        Task<string> CheckUsernameAndUseremailAvailability(string username, string email);
        Task<bool> CompleteRegister(string regEmail, string regUsername, string regPassword);
    }
}

namespace Forum.Contracts
{
    public interface IRegisterRepository
    {
        Task<bool> CheckUsernameAvailability(string username);
        Task<bool> CompleteRegister(string regEmail, string regUsername, string regPassword);
    }
}

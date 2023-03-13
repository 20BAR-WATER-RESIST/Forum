using Forum.Models;

namespace Forum.Contracts
{
    public interface ICommentRepository
    {
        Task<List<Comment>> LoadPlotComments(int topicId, int currentPage);
        Task<int> GetCommentAmmountPerTopic(int id);
        Task<List<Comment>> LoadUserProfileComments(string name);
        //Task<List<(string CommentText, DateTime CommentAddedTime, bool IsActive, string UserName)>> LoadPlotsComments(int id, int currentPage);
    }
}

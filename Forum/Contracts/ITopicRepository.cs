using Forum.Models;
using Forum.Models.ReportSystem;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface ITopicRepository
    {
        Task<List<Topic>> LoadPlot(int id);
        Task<List<Topic>> LoadUserProfileTopics(string name);
        Task<List<Topic>> LoadIndexPageTopics();
        Task<List<Topic>> LatestHotTopics();
        Task<List<Topic>> LoadBoardPageTopics(int id, int currentPage);
        Task<List<Topic>> BoardLatestHotTopics(int id);
        Task<List<Topic>> SearchTopics(string text);
        Task<List<ReportCategory>> LoadTopicReportCategories();
    }
}

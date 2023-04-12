using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models.ReportSystem;

namespace Forum.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DefaultDbContext _context;

        public ReportRepository(DefaultDbContext context)
        {
            _context = context;
        }

        //public async Task<string> SendReport(string repoAuth, int catID, string repMessage, int targID, string repoType)
        //{
        //    using(var connection = _context.CreateConnection())
        //    {
        //        var querry = @"insert into reportbase (ReportType,ReportCategoryID,ReportAuthor,ReportMessage,ReportAddedDate,ReportTargetID,IsActive)
        //                       values(@RT,@CI,(SELECT u.UserID FROM users u WHERE u.UserName = @RA),@RM,CURRENT_TIMESTAMP,@TI,true);";

        //        var result = await connection.QuerySingleOrDefault(querry, new { RA = repoAuth, CI = catID, RM = repMessage, TI = targID, RT = repoType });

        //        return "Everythink goes ok!";
        //    }
        //}

        public async Task<string> SendReport(string repoAuth, int catID, string repMessage, int targID, string repoType)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"insert into reportbase (ReportType,ReportCategoryID,ReportAuthor,ReportMessage,ReportAddedDate,ReportTargetID,IsActive)
                      values(@RT,@CI,(SELECT u.UserID FROM users u WHERE u.UserName = @RA),@RM,CURRENT_TIMESTAMP,@TI,true);";

                var result = await connection.ExecuteAsync(query, new { RA = repoAuth, CI = catID, RM = repMessage, TI = targID, RT = repoType });

                return "Everything goes ok!";
            }
        }

    }
}


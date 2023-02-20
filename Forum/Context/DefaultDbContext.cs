using Forum.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Forum.Context
{
    public class DefaultDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DefaultDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ForumConnectionString");
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}

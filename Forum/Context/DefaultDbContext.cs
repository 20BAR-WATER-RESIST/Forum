using Forum.Models;
using Forum.Security;
using MySql.Data.MySqlClient;
using System.Data;

namespace Forum.Context
{
    public class DefaultDbContext : GetConnection
    {
        private readonly string _connectionString;

        public DefaultDbContext()
        {
            _connectionString = AccessSecretVersion();
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}

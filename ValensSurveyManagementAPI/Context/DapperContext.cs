using System;
using System.Data;
using System.Data.SqlClient;

namespace ValensSurveyManagementAPI.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuation)
        {
            _configuration = configuation;
            _connectionString = _configuration.GetConnectionString("DatabaseConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}


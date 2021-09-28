using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string connectionName = "default")
        {
            return _config.GetConnectionString(connectionName);
        }

        public async Task<IEnumerable<T>> LoadData<T>(string sql, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return await cnn.QueryAsync<T>(sql, param);
            }
        }

        public async Task<int> SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return await cnn.ExecuteAsync(sql, data);
            }
        }
    }
}

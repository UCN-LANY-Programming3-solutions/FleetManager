using FleetManager.DataAccessLayer;
using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Tests
{
    class SqlServerDataContext : IDataContext<IDbConnection>
    {
        private readonly string _connectionString;

        public SqlServerDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SupportedContextTypes SupportedContext => SupportedContextTypes.SqlServer;

        public IDbConnection Open()
        {
            SqlConnection conn = new(_connectionString);
            conn.Open();
            return conn;
        }           
    }
}

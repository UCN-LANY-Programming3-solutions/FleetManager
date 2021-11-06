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

        public IDbConnection Open()
        {
            SqlConnection conn = new(_connectionString);
            conn.Open();
            return conn;
        }           

        //public static SqlServerDataContext Initialize(string connectionString)
        //{
        //    return new SqlServerDataContext(connectionString);
        //}

        //public SqlServerDataContext Feed()
        //{
        //    return this;
        //}

        //public IDataContext<IDbConnection> As<TInterface>() where TInterface : class
        //{
        //    return this;
        //}
    }
}

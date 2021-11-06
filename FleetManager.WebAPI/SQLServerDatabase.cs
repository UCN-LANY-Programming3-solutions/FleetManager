using FleetManager.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.WebAPI
{
    internal class SQLServerDatabase : IDataContext
    {
        public static string ConnectionString => @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_WPF; Integrated Security=true";

        public static IDataContext Create()
        {
            return new SQLServerDatabase();
        }

        public IDbConnection OpenConnection()
        {
            SqlConnection conn = new(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}

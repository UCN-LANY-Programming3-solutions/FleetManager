using FleetManager.DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FleetManager.WebAPI
{
    internal class SQLServerDataContext : IDataContext<IDbConnection>
    {
        public static string ConnectionString => @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_WPF; Integrated Security=true";

        public SupportedContextTypes SupportedContext => SupportedContextTypes.SqlServer;

        public IDbConnection Open()
        {
            SqlConnection conn = new(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}

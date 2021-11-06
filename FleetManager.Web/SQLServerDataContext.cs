using FleetManager.DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace FleetManager.Web
{
    public class SQLServerDataContext : IDataContext<IDbConnection>
    {
        public static string ConnectionString => $@"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_Web; Integrated Security=true";

        public static IDataContext Create()
        {
            return new SQLServerDataContext();
        }

        public IDbConnection Open()
        {
            IDbConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}

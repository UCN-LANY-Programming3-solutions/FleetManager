using FleetManager.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayerTests
{
    class SqlServerDataContext : IDataContext<IDbConnection>
    {
        private readonly string _connectionString = @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_test_{Guid.NewGuid()}; Integrated Security=true";

        internal static SqlServerDataContext Create()
        {
            SqlServerDataContext dataContext = new();
            Database.Version.Upgrade(dataContext.ConnectionString);
            return dataContext;
        }

        internal static void Destroy(IDataContext dataContext)
        {
            string connectionString = ((SqlServerDataContext)dataContext).ConnectionString;
            Database.Version.Drop(connectionString);
        }

        public string ConnectionString => _connectionString;

        public IDbConnection Open()
        {
            SqlConnection conn = new(_connectionString);
            conn.Open();
            return conn;
        }
    }
}

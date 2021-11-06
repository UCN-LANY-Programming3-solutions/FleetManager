using FleetManager.DataAccessLayer.Tests.DataContextBuilders;
using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Tests.UnitTests
{
    [TestClass]
    public class SqlServerDataContextBuilderTests
    {
        private string _connectionString = @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_SqlServerDataContextBuilderTests_{Guid.NewGuid()}; Integrated Security=true";
        private static readonly List<Action> _dropDatabaseActions = new();

        [ClassCleanup]
        public static void ExecuteCleanUp()
        {
            Parallel.Invoke(_dropDatabaseActions.ToArray());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dropDatabaseActions.Add(() => Database.Version.Drop(_connectionString));
        }

        [TestMethod]
        public void ShouldBuildSqlServerDataContext()
        {
            IDataContext dataContext = DataContextBuilder.For
                .SqlServer(_connectionString)
                .Initialize(() => new SqlServerDataContext(_connectionString))
                .Feed<Location>("locations.json")
                .Feed<Car>("cars.json")
                .Build<IDataContext>();

            Assert.IsNotNull(dataContext);
            Assert.IsInstanceOfType(dataContext, typeof(SqlServerDataContext));
        }
    }
}

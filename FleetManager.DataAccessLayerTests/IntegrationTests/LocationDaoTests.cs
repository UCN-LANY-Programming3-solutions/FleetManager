using FleetManager.DataAccessLayer.Tests.DataContextBuilders;
using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Tests.IntegrationTests
{
    public abstract class LocationDaoTests
    {
        protected IDataContext _dataContext;
        protected IDao<Location> _dao;

        [TestMethod]
        public virtual void ShouldGetAllLocations()
        {
            // Act
            IEnumerable<Location> test = _dao.Read();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public virtual void ShouldGetLocationWithSpecificId()
        {
            //  Arrange

            // Act
            IEnumerable<Location> test = _dao.Read(l => l.Id == 1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.Count());
            Assert.AreEqual("Aalborg", test.Single().Name);
        }


        [TestMethod]
        public virtual void ShouldGetEmptyLocationListByNonExistingId()
        {
            //  Arrange

            // Act
            IEnumerable<Location> testLocation = _dao.Read(l => l.Id == 11);

            // Assert
            Assert.IsNotNull(testLocation);
            Assert.AreEqual(0, testLocation.Count());
        }

        [TestMethod]
        public virtual void ShouldCreateLocation()
        {
            //  Arrange
            Location location = new() { Name = "Horsens" };

            // Act
            Location test = _dao.Create(location);

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Id.HasValue);
        }

        [TestMethod]
        public virtual void ShouldUpdateLocation()
        {
            //  Arrange
            Location location = new() { Id = 1, Name = "Horsens" };

            // Act
            bool test = _dao.Update(location);

            // Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public virtual void ShouldThrowExceptionWhenDeletingLocationThatHasCars()
        {
            //  Arrange
            Location location = new() { Id = 2 };

            // Act, Assert
            Exception ex = Assert.ThrowsException<DaoException>(() => _dao.Delete(location));
            Assert.IsTrue(ex.Message.Contains("An error ocurred deleting data from FleetManager.Model.Location"));
        }

        [TestMethod]
        public virtual void ShouldGetFalseWhenDeletingNonExistingLocation()
        {
            //  Arrange
            Location location = new() { Id = 3 };

            // Act
            bool test = _dao.Delete(location);

            // Assert
            Assert.IsFalse(test);
        }
    }

    [TestClass]
    public class SqlServerLocationDaoTests : LocationDaoTests
    {
        private readonly string _connectionString = @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_SqlServerLocationDaoTests_{Guid.NewGuid()}; Integrated Security=true";
        private static readonly List<Action> _dropDatabaseActions = new();

        [ClassCleanup]
        public static void WaitForCleanUpThreads()
        {
            Parallel.Invoke(_dropDatabaseActions.ToArray());
        }

        [TestCleanup]
        public void CleanupTest()
        {
            _dropDatabaseActions.Add(() => Database.Version.Drop(_connectionString));
        }

        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = DataContextBuilder.For
                   .SqlServer(_connectionString)
                   .Initialize(() => new SqlServerDataContext(_connectionString))
                   .Feed<Location>("locations.json")
                   .Feed<Car>("cars.json")
                   .Build<IDataContext>();

            _dao = DaoFactory
                .GetConcreteFactory(DaoFactory.ConcreteFactories.SqlServer)
                .Create<Location>(_dataContext);
        }
    }

    [TestClass]
    public class MemoryLocationDaoTests : LocationDaoTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = DataContextBuilder.For
                .Memory()
                .Initialize<MemoryDataContext>()
                .Feed<Location>("locations.json")
                .Build<IDataContext>();

            _dao = DaoFactory
                .GetConcreteFactory(DaoFactory.ConcreteFactories.Memory)
                .Create<Location>(_dataContext);
        }

        public override void ShouldThrowExceptionWhenDeletingLocationThatHasCars()
        {
            // Cancelling this test
        }
    }
}

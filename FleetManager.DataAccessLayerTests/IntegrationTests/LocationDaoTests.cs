using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayer.Tests
{
    public abstract class LocationDaoTests
    {
        protected IDataContext _dataContext;
        protected IDao<Location> _dao;

        [TestMethod]
        public virtual void ShouldGetAllLocationsTest()
        {
            // Act
            IEnumerable<Location> test = _dao.Read();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public virtual void ShouldGetLocationByIdTest()
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
        public virtual void ShouldGetEmptyLocationListByNonExistingIdTest()
        {
            //  Arrange

            // Act
            IEnumerable<Location> testLocation = _dao.Read(l => l.Id == 11);

            // Assert
            Assert.IsNotNull(testLocation);
            Assert.AreEqual(0, testLocation.Count());
        }

        [TestMethod]
        public virtual void ShouldCreateLocationTest()
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
        public virtual void ShouldUpdateLocationTest()
        {
            //  Arrange
            Location location = new() { Id = 1, Name = "Horsens" };

            // Act
            bool test = _dao.Update(location);

            // Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public virtual void ShouldThrowExceptionWhenDeletingLocationThatHasCarsTest()
        {
            //  Arrange
            Location location = new() { Id = 2 };

            // Act, Assert
            Exception ex = Assert.ThrowsException<DaoException>(() => _dao.Delete(location));
            Assert.IsTrue(ex.Message.Contains("An error ocurred deleting data from FleetManager.Model.Location"));
        }

        [TestMethod]
        public virtual void ShouldGetFalseWhenDeletingNonExistingLocationTest()
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
        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = SqlServerDataContext.Create();
            _dao = DaoFactory
                .GetConcreteFactory(DaoFactory.ConcreteFactories.SqlServer)
                .Create<Location>(_dataContext);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            SqlServerDataContext.Destroy(_dataContext);
        }
    }

    [TestClass]
    public class MemoryLocationDaoTests : LocationDaoTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = TupleDataContext.Create();
            _dao = DaoFactory.GetConcreteFactory(DaoFactory.ConcreteFactories.Memory).Create<Location>(_dataContext);
        }

        public override void ShouldThrowExceptionWhenDeletingLocationThatHasCarsTest()
        {
            // Cancelling this test
        }
    }
}

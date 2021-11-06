using FleetManager.Model;
using Lanysom.DataContextBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Tests.IntegrationTests
{
    public abstract class CarDaoTests
    {
        protected IDataContext _dataContext;
        protected IDao<Car> _dao;

        [TestMethod]
        public virtual void ShouldGetAllCars()
        {
            // Act
            IEnumerable<Car> test = _dao.Read();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public virtual void ShouldGetCarWithSpecificId()
        {
            // Act
            IEnumerable<Car> test = _dao.Read(c => c.Id == 1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.Count());
            Assert.AreEqual("Ford", test.Single().Brand);
        }

        [TestMethod]
        public virtual void ShouldGetEmptyListByNonExistingId()
        {
            // Act
            IEnumerable<Car> test = _dao.Read(c => c.Id == 11);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(0, test.Count());
        }

        [TestMethod]
        public virtual void ShouldCreateCar()
        {
            //  Arrange
            Car car = new() { Brand = "Hyundai", Id = 3, Mileage = 32000 };

            // Act
            Car test = _dao.Create(car);

            // Assert
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public virtual void ShouldUpdateCar()
        {
            //  Arrange
            Car car = new() { Id = 1, Brand = "Ford", Mileage = 45000, Location = null };

            // Act
            bool test = _dao.Update(car);

            // Assert
            Assert.IsTrue(test);

            Car updatedCar = _dao.Read(c => c.Id == car.Id).Single();

            Assert.AreEqual(car.Brand, updatedCar.Brand);
            Assert.AreEqual(car.Mileage, updatedCar.Mileage);
            Assert.IsNull(updatedCar.Location);
        }

        [TestMethod]
        public virtual void ShouldDeleteCar()
        {
            //  Arrange
            Car car = new() { Id = 2 };

            // Act
            bool test = _dao.Delete(car);

            // Assert
            Assert.IsTrue(test);
        }
    }

    [TestClass]
    public class SqlServerCarDaoTests : CarDaoTests
    {
        private readonly string _connectionString = @$"Data Source=(localdb)\mssqllocaldb; Initial Catalog=FleetManager_SqlServerCarDaoTests_{Guid.NewGuid()}; Integrated Security=true";
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
            Database.Version.Upgrade(_connectionString);

            _dataContext = DataContextBuilder.For
                .SqlServer(_connectionString)
                .Initialize(() => new SqlServerDataContext(_connectionString))
                .Feed<Location>("locations.json")
                .Feed<Car>("cars.json")
                .Build<IDataContext>();

            _dao = DaoFactory.Create<Car>(_dataContext, DaoFactory.ConcreteFactories.SqlServer);
        }
    }

    [TestClass]
    public class MemoryCarDaoTests : CarDaoTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = DataContextBuilder.For
                .Memory()
                .Initialize<MemoryDataContext>()
                //.Feed<Location>("locations.json")
                .Feed<Car>("cars.json")
                .Build<IDataContext>();

            _dao = DaoFactory.Create<Car>(_dataContext, DaoFactory.ConcreteFactories.Memory);
        }
    }
}

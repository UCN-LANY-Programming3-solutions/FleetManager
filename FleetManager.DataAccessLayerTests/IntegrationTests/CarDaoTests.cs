using FleetManager.DataAccessLayer;
using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayer.Tests
{
    public abstract class CarDaoTests
    {
        protected IDataContext _dataContext;
        protected IDao<Car> _dao;

        [TestMethod]
        public virtual void ShouldGetAllCarsTest()
        {
            // Act
            IEnumerable<Car> test = _dao.Read();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public virtual void ShouldGetCarWithSpecificIdTest()
        {
            // Act
            IEnumerable<Car> test = _dao.Read(c => c.Id == 1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.Count());
            Assert.AreEqual("Ford", test.Single().Brand);
        }

        [TestMethod]
        public virtual void ReadByNonExistingIdTest()
        {
            // Act
            IEnumerable<Car> test = _dao.Read(c => c.Id == 11);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(0, test.Count());
        }

        [TestMethod]
        public virtual void CreateTest()
        {
            //  Arrange
            Car car = new() { Brand = "Hyundai", Id = 3, Mileage = 32000 };

            // Act
            Car test = _dao.Create(car);

            // Assert
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public virtual void ShouldUpdateCarTest()
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
        public virtual void DeleteTest()
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
        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = SqlServerDataContext.Create();
            _dao = DaoFactory.GetConcreteFactory(DaoFactory.ConcreteFactories.SqlServer).Create<Car>(_dataContext);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            SqlServerDataContext.Destroy(_dataContext);
        }
    }

    [TestClass]
    public class MemoryCarDaoTests : CarDaoTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = TupleDataContext.Create();
            _dao = DaoFactory.GetConcreteFactory(DaoFactory.ConcreteFactories.Memory).Create<Car>(_dataContext);
        }
    }
}

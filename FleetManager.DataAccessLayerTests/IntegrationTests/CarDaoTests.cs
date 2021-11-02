using FleetManager.DataAccessLayer;
using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayerTests
{
    [TestClass]
    public class CarDaoTests
    {
        private IDataContext _dataContext;

        [TestInitialize]
        public void InitializeTest()
        {
            _dataContext = SqlServerDataContext.Create();            
        }

        [TestCleanup]
        public void CleanupTest()
        {
            SqlServerDataContext.Destroy(_dataContext);
        }

        [TestMethod]
        public void ReadAllTest()
        {
            //  Arrange
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);

            // Act
            IEnumerable<Car> test = dao.Read();

            // Assert
            Assert.IsNotNull(test);
            Assert.IsTrue(test.Count() == 2);
        }

        [TestMethod]
        public void ReadByIdTest()
        {
            //  Arrangeu
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);

            // Act
            IEnumerable<Car> test = dao.Read(c => c.Id == 1);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(1, test.Count());
            Assert.AreEqual("Ford", test.Single().Brand);
        }

        [TestMethod]
        public void ReadByNonExistingIdTest()
        {
            //  Arrange
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);

            // Act
            IEnumerable<Car> test = dao.Read(c => c.Id == 11);

            // Assert
            Assert.IsNotNull(test);
            Assert.AreEqual(0, test.Count());
        }

        [TestMethod]
        public void CreateTest()
        {
            //  Arrange
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);
            Car car = new() { Brand = "Hyundai", Id = 3, Mileage = 32000 };

            // Act
            int test = dao.Create(car);

            // Assert
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //  Arrange
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);
            Car car = new() { Id = 1, Brand = "Ford", Mileage = 45000 };

            // Act
            int test = dao.Update(car);

            // Assert
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //  Arrange
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);
            Car car = new() { Id = 2 };

            // Act
            int test = dao.Delete(car);

            // Assert
            Assert.AreEqual(1, test);
        }
    }
}

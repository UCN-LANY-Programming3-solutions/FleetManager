using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;

namespace FleetManager.DataAccessLayer.Tests.UnitTests
{
    public abstract class DaoFactoryTests
    {
        protected IDataContext _dataContext;
        protected SupportedContextTypes _concreteFactory;

        [TestMethod()]
        public void ShouldCreateCarDaoTest()
        {
            // Act
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void ShouldCreateLocationDaoTest()
        {
            // Act
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void ShouldThrowArgumentNullExceptionWhenDataContextIsNull()
        {
            Exception e = Assert.ThrowsException<ArgumentNullException>(() => DaoFactory.Create<Location>(null));

            Assert.IsTrue(e.Message.Contains("dataContext"));
        }
    }

    [TestClass]
    public class SqlServerDaoFactoryTests : DaoFactoryTests
    {
        [TestInitialize]
        public void Setup()
        {
            Mock<IDataContext<IDbConnection>> mockedDataContext = new();
            mockedDataContext.Setup(c => c.SupportedContext).Returns(SupportedContextTypes.SqlServer);

            _dataContext = mockedDataContext.Object;
        }
    }

    [TestClass]
    public class MemoryDaoFactoryTests : DaoFactoryTests
    {
        [TestInitialize]
        public void Setup()
        {
            Mock<IDataContext<Tuple<IList<Car>, IList<Location>>>> mockedDataContext = new();
            mockedDataContext.Setup(c => c.SupportedContext).Returns(SupportedContextTypes.Memory);

            _dataContext = mockedDataContext.Object;
        }
    }
}
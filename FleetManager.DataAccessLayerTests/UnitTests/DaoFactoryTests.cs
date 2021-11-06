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
        protected DaoFactory.ConcreteFactories _concreteFactory;

        [TestMethod()]
        public void ShouldCreateCarDaoTest()
        {
            // Act
            IDao<Car> dao = DaoFactory.Create<Car>(_dataContext, _concreteFactory);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void ShouldCreateLocationDaoTest()
        {
            // Act
            IDao<Location> dao = DaoFactory.Create<Location>(_dataContext, _concreteFactory);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void ShouldThrowArgumentNullExceptionWhenDataContextIsNull()
        {
            Exception e = Assert.ThrowsException<ArgumentNullException>(() => DaoFactory.Create<Location>(null, _concreteFactory));

            Assert.IsTrue(e.Message.Contains("dataContext"));
        }
    }

    [TestClass]
    public class SqlServerDaoFactoryTests : DaoFactoryTests
    {
        [TestInitialize]
        public void Setup()
        {
            _concreteFactory = DaoFactory.ConcreteFactories.SqlServer;
            _dataContext = Mock.Of<IDataContext<IDbConnection>>();
        }
    }

    [TestClass]
    public class MemoryDaoFactoryTests : DaoFactoryTests
    {
        [TestInitialize]
        public void Setup()
        {
            _concreteFactory = DaoFactory.ConcreteFactories.Memory;
            _dataContext = Mock.Of<IDataContext<Tuple<IList<Car>, IList<Location>>>>();
        }
    }
}
﻿using FleetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FleetManager.DataAccessLayer.Tests
{
    public abstract class DaoFactoryTests
    {
        protected IDataContext _dataContext;
        protected DaoFactory.ConcreteFactories _concreteFactory;

        [TestMethod()]
        public void ShouldCreateCarDaoTest()
        {
            // Act
            IDao<Car> dao = DaoFactory
                .GetConcreteFactory(_concreteFactory)
                .Create<Car>(_dataContext);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void ShouldCreateLocationDaoTest()
        {
            // Act
            IDao<Location> dao = DaoFactory
                .GetConcreteFactory(_concreteFactory)
                .Create<Location>(_dataContext);

            // Assert
            Assert.IsNotNull(dao);
        }

        [TestMethod]
        public void ShouldThrowArgumentNullExceptionWhenDataContextIsNull()
        {
            Exception e = Assert.ThrowsException<ArgumentNullException>(() => DaoFactory
                .GetConcreteFactory(_concreteFactory)
                .Create<Location>(null));

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
            _dataContext = SqlServerDataContext.Create();
        }
    }

    [TestClass]
    public class MemoryDaoFactoryTests : DaoFactoryTests
    {
        [TestInitialize]
        public void Setup()
        {
            _concreteFactory = DaoFactory.ConcreteFactories.Memory;
            _dataContext = TupleDataContext.Create();
        }
    }
}
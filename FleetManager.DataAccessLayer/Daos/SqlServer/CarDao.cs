using Dapper;
using FleetManager.DataAccessLayer.Daos.SqlServer.Entities;
using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FleetManager.DataAccessLayer.Daos.SqlServer
{
    internal class CarDao : BaseDao<IDbConnection>, IDao<Car>
    {
        public CarDao(IDataContext dataContext) : base(dataContext as IDataContext<IDbConnection>) { }

        public IEnumerable<Car> Read(Predicate<Car> predicate = null)
        {
            string selectCarSQL = "SELECT * " +
              "FROM Cars " +
              "LEFT JOIN Locations ON Locations.Id = Cars.LocationId ";

            using IDbConnection connection = DataContext.Open();
            IEnumerable<Car> cars = connection.Query<Car, Location, Car>(selectCarSQL, (c, l) =>
            {
                c.Location = l;
                return c;
            });
            return predicate == null ? cars : cars.Where(c => predicate(c));
        }

        public int Create(Car model)
        {
            try
            {
                string insertCarSQL = "INSERT INTO Cars (Brand, Mileage, Reserved, LocationId) VALUES (@brand, @mileage, @reserved, @locationId)";

                using IDbConnection connection = DataContext.Open();
                return connection.Execute(insertCarSQL, new CarEntity(model));
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred inserting data from {model}", ex);
            }
        }

        public int Update(Car model)
        {
            try
            {
                using var connection = DataContext.Open();
                return connection.Execute("Update Cars SET Brand = @brand, Mileage = @mileage, Reserved = @reserved, LocationId = @locationId WHERE Id = @id", new CarEntity(model));
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred updating data from {model}", ex);
            }
        }

        public int Delete(Car model)
        {
            try
            {
                using var connection = DataContext.Open();
                return connection.Execute("DELETE FROM Cars WHERE Id = @id", model);
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred deleting data from {model}", ex);
            }
        }
    }
}

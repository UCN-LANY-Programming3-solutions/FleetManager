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
        public CarDao(IDataContext dataContext) : base(dataContext as IDataContext<IDbConnection>)
        {
        }

        public IEnumerable<Car> Read(Func<Car, bool> predicate = null)
        {
            string selectCarSQL = "SELECT * " +
              "FROM Cars " +
              "LEFT JOIN Locations ON Locations.Id = Cars.LocationId ";

            using IDbConnection connection = DataContext.Open();
            IEnumerable<Car> cars = connection.Query<CarEntity, LocationEntity, Car>(selectCarSQL, (c, l) =>
            {
                Car car = c.Map();
                car.Location = l.Map();
                return car;
            });
            return predicate == null ? cars : cars.Where(predicate);
        }

        public Car Create(Car model)
        {
            try
            {
                string insertCarSQL = "INSERT INTO Cars (Brand, Mileage, Reserved, LocationId) " +
                    "VALUES (@brand, @mileage, @reserved, @locationId); " +
                    "SELECT SCOPE_IDENTITY();";

                using IDbConnection connection = DataContext.Open();
                model.Id = connection.ExecuteScalar<int>(insertCarSQL, model.Map());
                return model;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred inserting data from {model}", ex);
            }
        }

        public bool Update(Car model)
        {
            try
            {
                string updateCarSql = "Update Cars " +
                    "SET Brand = @brand, Mileage = @mileage, Reserved = @reserved, LocationId = @locationId " +
                    "WHERE Id = @id";

                using var connection = DataContext.Open();
                int rowsAffected = connection.Execute(updateCarSql, model.Map());
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred updating data from {model}", ex);
            }
        }

        public bool Delete(Car model)
        {
            try
            {
                string deleteCarSql = "DELETE FROM Cars WHERE Id = @id";

                using var connection = DataContext.Open();
                int rowsAffected = connection.Execute(deleteCarSql, model.Map());
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred deleting data from {model}", ex);
            }
        }
    }
}

using Dapper;
using FleetManager.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FleetManager.DataAccessLayer.Daos
{
    internal class CarDao : BaseDao<Car>
    {
        public CarDao(IDataContext dataContext) : base(dataContext) { }

        public override IEnumerable<Car> ReadAll()
        {
            string selectCarSQL = "SELECT * " +
              "FROM Cars " +
              "LEFT JOIN Locations ON Locations.Id = Cars.LocationId ";

            using IDbConnection connection = DataContext.OpenConnection();
            return connection.Query<Car, Location, Car>(selectCarSQL, (c, l) =>
            {
                c.Location = l;
                return c;
            });
        }

        public override Car ReadById(int id)
        {
            string selectCarSQL = "SELECT * " +
               "FROM Cars " +
               "LEFT JOIN Locations ON Locations.Id = Cars.LocationId " +
               "WHERE Cars.Id = @id";

            using IDbConnection connection = DataContext.OpenConnection();
            return connection.Query<Car, Location, Car>(selectCarSQL, (c, l) =>
            {
                c.Location = l;
                return c;
            }, new { id }).SingleOrDefault();
        }

        public override int Create(Car model)
        {
            try
            {
                string insertCarSQL = "INSERT INTO Cars (Brand, Mileage, Reserved, LocationId) VALUES (@brand, @mileage, @reserved, @locationId)";

                using IDbConnection connection = DataContext.OpenConnection();
                return connection.Execute(insertCarSQL, model);
            }
            catch (Exception ex)
            {
                throw new DaoException(model, ex);
            }
        }

        public override int Update(Car model)
        {
            try
            {
                using var connection = DataContext.OpenConnection();
                return connection.Execute("Update Cars SET Brand = @brand, Mileage = @mileage, Reserved = @reserved WHERE Id = @id", model);
            }
            catch (Exception ex)
            {
                throw new DaoException(model, ex);
            }
        }

        public override int Delete(Car model)
        {
            try
            {
                using var connection = DataContext.OpenConnection();
                return connection.Execute("DELETE FROM Cars WHERE Id = @id", model);
            }
            catch (Exception ex)
            {
                throw new DaoException(model, ex);
            }
        }
    }
}

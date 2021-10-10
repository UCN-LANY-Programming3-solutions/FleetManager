using Dapper;
using FleetManager.Entities;
using System;
using System.Collections.Generic;

namespace FleetManager.DataAccessLayer.Daos
{
    internal class CarDao : BaseDao<Car>
    {
        public CarDao(IDataContext dataContext) : base(dataContext) { }

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

        public override IEnumerable<Car> ReadAll()
        {
            using var connection = DataContext.OpenConnection();
            return connection.Query<Car>("SELECT * FROM Cars");
        }

        public override Car ReadById(int id)
        {
            using var connection = DataContext.OpenConnection();
            return connection.QuerySingle<Car>("SELECT * FROM Cars WHERE Id = @id", new { id });
        }

        public override int Create(Car model)
        {
            try
            {
                using var connection = DataContext.OpenConnection();
                return connection.Execute("INSERT INTO Cars (Brand, Mileage, Reserved) VALUES (@brand, @mileage, @reserved)", model);
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
    }
}

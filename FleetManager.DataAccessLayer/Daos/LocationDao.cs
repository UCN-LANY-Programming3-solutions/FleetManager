using Dapper;
using FleetManager.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace FleetManager.DataAccessLayer.Daos
{
    internal class LocationDao : BaseDao<Location>
    {
        public LocationDao(IDataContext dataContext) : base(dataContext) { }

        public override int Delete(Location model)
        {
            try
            {
                using IDbConnection conn = DataContext.OpenConnection();
                return conn.Execute("DELETE FROM Locations WHERE Id = @id", model);
            }
            catch (Exception ex)
            {
                throw new DaoException(model, ex);
            }
        }

        public override IEnumerable<Location> ReadAll()
        {
            using IDbConnection conn = DataContext.OpenConnection();
            return conn.Query<Location>("SELECT * FROM Locations");
        }

        public override Location ReadById(int id)
        {
            using IDbConnection conn = DataContext.OpenConnection();
            return conn.QuerySingle<Location>("SELECT * FROM Locations WHERE Id = @id", new { id });
        }

        public override int Create(Location model)
        {
            try
            {
                using IDbConnection conn = DataContext.OpenConnection();
                return conn.Execute("INSERT INTO Locations VALUES (@name)", model);
            }
            catch (Exception ex)
            {
                throw new DaoException(model, ex);
            }
        }

        public override int Update(Location model)
        {
            try
            {
                using IDbConnection conn = DataContext.OpenConnection();
                return conn.Execute("UPDATE Locations SET Name = @name WHERE Id = @id", model);
            }
            catch (Exception ex)
            {
                throw new DaoException(model, ex);
            }
        }
    }
}

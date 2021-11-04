using Dapper;
using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FleetManager.DataAccessLayer.Daos.SqlServer
{
    internal class LocationDao : BaseDao<IDbConnection>, IDao<Location>
    {
        public LocationDao(IDataContext dataContext) : base(dataContext as IDataContext<IDbConnection>) { }

        public Location Create(Location model)
        {
            try
            {
                using IDbConnection conn = DataContext.Open();
                model.Id = conn.ExecuteScalar<int>("INSERT INTO Locations VALUES (@name); SELECT SCOPE_IDENTITY();", model);
                return model;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred inserting data from {model}", ex);
            }
        }

        public IEnumerable<Location> Read(Func<Location, bool> predicate = null)
        {
            using IDbConnection conn = DataContext.Open();
            IEnumerable<Location> locations = conn.Query<Location>("SELECT * FROM Locations");
            return predicate == null ? locations : locations.Where(predicate);

        }

        public bool Update(Location model)
        {
            try
            {
                using IDbConnection conn = DataContext.Open();
                return conn.Execute("UPDATE Locations SET Name = @name WHERE Id = @id", model) == 1;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred updating data from {model}", ex);
            }
        }

        public bool Delete(Location model)
        {
            try
            {
                using IDbConnection conn = DataContext.Open();
                return conn.Execute("DELETE FROM Locations WHERE Id = @id", model) == 1;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred deleting data from {model}", ex);
            }
        }
    }
}

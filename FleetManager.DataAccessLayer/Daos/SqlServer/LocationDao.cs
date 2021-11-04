using Dapper;
using FleetManager.DataAccessLayer.Daos.SqlServer.Entities;
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
                string insertLocationSql = "INSERT INTO Locations VALUES (@name); SELECT SCOPE_IDENTITY();";

                using IDbConnection conn = DataContext.Open();
                model.Id = conn.ExecuteScalar<int>(insertLocationSql, model.Map());
                return model;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred inserting data from {model}", ex);
            }
        }

        public IEnumerable<Location> Read(Func<Location, bool> predicate = null)
        {
            string selectLocationSql = "SELECT * FROM Locations";

            using IDbConnection conn = DataContext.Open();
            IEnumerable<Location> locations = conn.Query<Location>(selectLocationSql);
            return predicate == null ? locations : locations.Where(predicate);
        }

        public bool Update(Location model)
        {
            try
            {
                string updateLocationSql = "UPDATE Locations SET Name = @name WHERE Id = @id";

                using IDbConnection conn = DataContext.Open();
                return conn.Execute(updateLocationSql, model.Map()) == 1;
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
                string deleteLocationSql = "DELETE FROM Locations WHERE Id = @id";

                using IDbConnection conn = DataContext.Open();
                return conn.Execute(deleteLocationSql, model.Map()) == 1;
            }
            catch (Exception ex)
            {
                throw new DaoException($"An error ocurred deleting data from {model}", ex);
            }
        }
    }
}

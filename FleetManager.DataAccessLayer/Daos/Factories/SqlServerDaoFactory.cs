using FleetManager.Model;
using System;

namespace FleetManager.DataAccessLayer.Daos.Factories
{
    class SqlServerDaoFactory : DaoFactory
    {
        public override IDao<TModel> CreateDao<TModel>(IDataContext dataContext)
        {
            return typeof(TModel) switch
            {
                var dao when dao == typeof(Car) => new SqlServer.CarDao(dataContext) as IDao<TModel>,
                var dao when dao == typeof(Location) => new SqlServer.LocationDao(dataContext) as IDao<TModel>,
                _ => throw new DaoException($"No DAO supporting {typeof(TModel).Name}"),
            };

        }
    }
}

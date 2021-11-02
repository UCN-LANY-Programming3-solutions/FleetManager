using FleetManager.DataAccessLayer.Daos;
using FleetManager.Model;

namespace FleetManager.DataAccessLayer
{
    public static class DaoFactory
    {
        public static IDao<TModel> Create<TModel>(IDataContext dataContext)
        {


            return typeof(TModel) switch
            {
                var dao when dao == typeof(Car) => new Daos.SqlServer.CarDao(dataContext) as IDao<TModel>,
                var dao when dao == typeof(Location) => new Daos.SqlServer.LocationDao(dataContext) as IDao<TModel>,
                _ => throw new DaoException(),
            };
        }
    }
}

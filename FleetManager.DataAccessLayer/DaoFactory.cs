using FleetManager.DataAccessLayer.Daos;
using FleetManager.Entities;

namespace FleetManager.DataAccessLayer
{
    public static class DaoFactory
    {
        public static IDao<TModel> Create<TModel>(IDataContext dataContext)
        {
            return typeof(TModel) switch
            {
                var dao when dao == typeof(Car) => new CarDao(dataContext) as IDao<TModel>,
                var dao when dao == typeof(Location) => new LocationDao(dataContext) as IDao<TModel>,
                _ => null,
            };
        }
    }
}

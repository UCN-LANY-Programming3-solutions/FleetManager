using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos.Factories
{
    class MemoryDaoFactory : DaoFactory
    {
        public override IDao<TModel> Create<TModel>(IDataContext dataContext)
        {
            return typeof(TModel) switch
            {
                var dao when dao == typeof(Car) => new Memory.CarDao(dataContext) as IDao<TModel>,
                var dao when dao == typeof(Location) => new Memory.LocationDao(dataContext) as IDao<TModel>,
                _ => throw new DaoException($"No DAO supporting {typeof(TModel).Name}"),
            };
        }
    }
}

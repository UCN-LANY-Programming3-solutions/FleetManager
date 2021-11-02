using System;
using System.Collections.Generic;

namespace FleetManager.DataAccessLayer.Daos
{
    internal abstract class BaseDao<TConnection>
    {
        public IDataContext<TConnection> DataContext { get; }

        public BaseDao(IDataContext<TConnection> dataContext)
        {
            DataContext = dataContext;
        }
    }
}

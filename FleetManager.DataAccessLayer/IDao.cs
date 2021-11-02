using System;
using System.Collections.Generic;

namespace FleetManager.DataAccessLayer
{
    public interface IDao<TModel>
    {
        int Create(TModel model);

        IEnumerable<TModel> Read(Predicate<TModel> predicate = null);

        int Update(TModel model);

        int Delete(TModel model);
    }
}

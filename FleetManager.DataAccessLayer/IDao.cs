using System;
using System.Collections.Generic;

namespace FleetManager.DataAccessLayer
{
    public interface IDao<TModel>
    {
        TModel Create(TModel model);

        IEnumerable<TModel> Read(Func<TModel, bool> predicate = null);

        bool Update(TModel model);

        bool Delete(TModel model);
    }
}

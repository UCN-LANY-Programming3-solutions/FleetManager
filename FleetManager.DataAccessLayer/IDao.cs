using System.Collections.Generic;

namespace FleetManager.DataAccessLayer
{
    public interface IDao<TModel>
    {
        int Create(TModel model);

        IEnumerable<TModel> ReadAll();

        TModel ReadById(int id);

        int Update(TModel model);

        int Delete(TModel model);
    }
}

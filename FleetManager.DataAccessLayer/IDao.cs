using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

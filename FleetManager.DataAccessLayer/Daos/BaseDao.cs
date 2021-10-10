using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos
{
    internal abstract class BaseDao<TModel> : IDao<TModel>
    {
        public IDataContext DataContext { get; }

        public BaseDao(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public abstract IEnumerable<TModel> ReadAll();
        public abstract TModel ReadById(int id);
        public abstract int Create(TModel model);
        public abstract int Update(TModel model);
        public abstract int Delete(TModel model);
    }
}

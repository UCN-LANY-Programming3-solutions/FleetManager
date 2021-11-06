using FleetManager.DataAccessLayer.Daos.Factories;
using System;

namespace FleetManager.DataAccessLayer
{
    public interface IDaoFactory
    {
        IDao<TModel> CreateDao<TModel>(IDataContext dataContext);
    }

    public abstract class DaoFactory : IDaoFactory
    {
        public static IDao<TModel> Create<TModel>(IDataContext dataContext)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            IDaoFactory factory = dataContext.SupportedContext switch
            {
                SupportedContextTypes.SqlServer => new SqlServerDaoFactory(),
                SupportedContextTypes.Memory => new MemoryDaoFactory(),
                _ => throw new DaoException($"{dataContext.SupportedContext} Factory not supported"),
            };

            return factory.CreateDao<TModel>(dataContext);
        }

        public abstract IDao<TModel> CreateDao<TModel>(IDataContext dataContext);
    }

    public enum SupportedContextTypes
    {
        SqlServer,
        Rest,
        Memory
    }
}

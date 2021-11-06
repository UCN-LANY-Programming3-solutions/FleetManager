using FleetManager.DataAccessLayer.Daos.Factories;

namespace FleetManager.DataAccessLayer
{
    public interface IDaoFactory
    {
        IDao<TModel> Create<TModel>(IDataContext dataContext);
    }

    public abstract class DaoFactory : IDaoFactory
    {
        public static IDao<TModel> Create<TModel>(IDataContext dataContext, ConcreteFactories factoryType)
        {
            IDaoFactory factory = factoryType switch
            {
                ConcreteFactories.SqlServer => new SqlServerDaoFactory(),
                ConcreteFactories.Memory => new MemoryDaoFactory(),
                _ => throw new DaoException($"{factoryType} Factory not supported"),
            };

            return factory.Create<TModel>(dataContext);
        }

        public abstract IDao<TModel> Create<TModel>(IDataContext dataContext);

        public enum ConcreteFactories
        {
            SqlServer,
            Rest, 
            Memory
        }
    }
}

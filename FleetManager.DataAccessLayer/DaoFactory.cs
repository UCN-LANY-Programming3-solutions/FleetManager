using FleetManager.DataAccessLayer.Daos.Factories;

namespace FleetManager.DataAccessLayer
{
    public interface IDaoFactory
    {
        IDao<TModel> Create<TModel>(IDataContext dataContext);
    }

    public abstract class DaoFactory : IDaoFactory
    {
        public static IDaoFactory GetConcreteFactory(ConcreteFactories factory)
        {
            return factory switch
            {
                ConcreteFactories.SqlServer => new SqlServerDaoFactory(),
                ConcreteFactories.Memory => new MemoryDaoFactory(),
                _ => throw new DaoException($"{factory} Factory not supported"),
            };
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

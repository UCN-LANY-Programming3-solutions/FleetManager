using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos.Memory
{
    class LocationDao : BaseDao<Tuple<IList<Car>, IList<Location>>>, IDao<Location>
    {
        public LocationDao(IDataContext dataContext) : base(dataContext as IDataContext<Tuple<IList<Car>, IList<Location>>>)
        {
        }

        public Location Create(Location model)
        {
            model.Id = (DataContext.Open().Item2.Max(l => l.Id) + 1) ?? 1;
            DataContext.Open().Item2.Add(model);

            return model;
        }

        public bool Delete(Location model)
        {
            Location oldLocation = DataContext.Open().Item2.SingleOrDefault(l => l.Id == model.Id);
            if (oldLocation != null)
            {
                return DataContext.Open().Item2.Remove(oldLocation);
            }
            return false;
        }

        public IEnumerable<Location> Read(Func<Location, bool> predicate = null)
        {
            return predicate == null ? DataContext.Open().Item2 : DataContext.Open().Item2.Where(predicate);
        }

        public bool Update(Location model)
        {
            Location updatedLocation = DataContext.Open().Item2.SingleOrDefault(l => l.Id == model.Id);
            if (updatedLocation != null)
            {
                updatedLocation.Name = model.Name;
                return true;
            }
            return false;
        }
    }
}

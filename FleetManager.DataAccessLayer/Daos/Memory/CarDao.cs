using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.DataAccessLayer.Daos.Memory
{
    class CarDao : BaseDao<Tuple<IList<Car>, IList<Location>>>, IDao<Car>
    {
        public CarDao(IDataContext dataContext) : base(dataContext as IDataContext<Tuple<IList<Car>, IList<Location>>>)
        {

        }

        public Car Create(Car model)
        {
            DataContext.Open().Item1.Add(model);

            return model;
        }

        public bool Delete(Car model)
        {
            Car deletedCar = DataContext.Open().Item1.Single(c => c.Id == model.Id);

            return DataContext.Open().Item1.Remove(deletedCar);
        }

        public IEnumerable<Car> Read(Func<Car, bool> predicate = null)
        {
            IEnumerable<Car> cars = DataContext.Open().Item1;
            return predicate == null ? cars.ToList() : cars.Where(predicate);
        }

        public bool Update(Car model)
        {
            Car updatedCar = DataContext.Open().Item1.Single(c => c.Id == model.Id);

            if (updatedCar == null)
                return false;
            
            updatedCar.Mileage = model.Mileage;
            updatedCar.Location = model.Location;
            updatedCar.Reserved = model.Reserved;

            return true;
        }
    }
}

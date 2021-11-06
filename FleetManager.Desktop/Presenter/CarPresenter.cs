using FleetManager.Desktop.Data;
using FleetManager.Desktop.Model;
using System.Collections.Generic;

namespace FleetManager.Desktop.Presenter
{
    public class CarPresenter
    {
        private readonly IDao<Car> _carData;
        private readonly IDao<Location> _locationData;

        public CarPresenter(IDao<Car> carData, IDao<Location> locationData)
        {
            _carData = carData;
            _locationData = locationData;
        }

        public IEnumerable<CarModel> GetAllCars()
        {
            foreach (Car car in _carData.Read())
            {
                yield return car.Map();
            }
        }

        public IEnumerable<LocationModel> GetAllLocations()
        {
            foreach (Location location in _locationData.Read())
            {
                yield return location.Map();
            }
        }

        public CarModel SaveCar(CarModel car)
        {
            if (car.Id.HasValue) // update car
            {
                _carData.Update(car.Map());
            }
            else // create car
            {
                _carData.Create(car.Map());
            }
            return car;
        }

        public bool DeleteCar(CarModel car)
        {
            return _carData.Delete(car.Map());
        }
    }
}

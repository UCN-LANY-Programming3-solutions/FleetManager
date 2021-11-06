using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Desktop.Model
{
    public class EditCarModel
    {
        private readonly CarModel _car;

        public EditCarModel(IEnumerable<LocationModel> locations): this(locations, new CarModel())
        {
        }

        public EditCarModel(IEnumerable<LocationModel> locations, CarModel selectedCar)
        {
            _car = selectedCar;
            Locations = locations;
        }

        public CarModel Car => _car;
        public string Brand { get => _car.Brand; set => _car.Brand = value; }
        public int Mileage { get => _car.Mileage; set => _car.Mileage = value; }
        public LocationModel Location { get=>_car.Location; set=>_car.Location = value; }

        public IEnumerable Locations { get; private set; }
    }
}

namespace FleetManager.Desktop.Model
{
    static class Mapper
    {
        public static Car Map(this CarModel car)
        {
            return new Car
            {
                Id = car.Id,
                Brand = car.Brand,
                Mileage = car.Mileage,
                Location = car.Location?.Map()
            };
        }

        public static Location Map(this LocationModel location)
        {
            return new Location
            {
                Id = location.Id,
                Name = location.Name
            };
        }

        public static CarModel Map(this Car car)
        {
            return new CarModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Mileage = car.Mileage,
                Location = car.Location?.Map()
            };
        }

        public static LocationModel Map(this Location location)
        {
            return new LocationModel
            {
                Id = location.Id,
                Name = location.Name
            };
        }
    }
}

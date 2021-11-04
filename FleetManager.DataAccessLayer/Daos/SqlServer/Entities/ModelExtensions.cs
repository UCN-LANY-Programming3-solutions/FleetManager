using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos.SqlServer.Entities
{
    static class ModelExtensions
    {
        public static Car Map(this CarEntity carEntity)
        {
            return new Car
            {
                Id = carEntity.Id,
                Brand = carEntity.Brand,
                Mileage = carEntity.Mileage,
                Reserved = carEntity.Reserved
            };
        }

        public static CarEntity Map(this Car car)
        {
            return new CarEntity
            {
                Id = car.Id,
                Brand = car.Brand,
                Mileage = car.Mileage,
                Reserved = car.Reserved,
                LocationId = car.Location?.Id
            };
        }

        public static Location Map(this LocationEntity locationEntity)
        {
            return new Location
            {
                Id = locationEntity.Id,
                Name = locationEntity.Name
            };
        }

        public static LocationEntity Map(this Location location)
        {
            return new LocationEntity
            {
                Id = location.Id,
                Name = location.Name
            };
        }
    }
}

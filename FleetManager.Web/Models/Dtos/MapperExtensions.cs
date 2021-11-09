using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web.Models.Dtos
{
    public static class MapperExtensions
    {
        #region Car -> CarListItemDto

        public static IEnumerable<CarListItemDto> Map(this IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            {
                yield return car.Map();
            }
        }

        public static CarListItemDto Map(this Car car)
        {
            return new CarListItemDto
            {
                CarId = car.Id.Value, 
                Brand = car.Brand, 
                LocationId = car.Location.Id.Value,
                LocationName = car.Location.Name
            };
        }
        #endregion

        #region Location -> LocationListItemDto
        public static IEnumerable<LocationListItemDto> Map(this IEnumerable<Location> locations)
        {
            return null;
        }
        
        #endregion
    }
}

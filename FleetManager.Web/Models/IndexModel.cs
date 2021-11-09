using FleetManager.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web.Models
{
    public class IndexModel
    {
        public IEnumerable<CarListItemDto> AvailableCars { get; set; }
        public IEnumerable<LocationListItemDto> AllLocations { get; set; }
    }
}

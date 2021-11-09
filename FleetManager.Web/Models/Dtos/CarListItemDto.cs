using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web.Models.Dtos
{
    public class CarListItemDto
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}

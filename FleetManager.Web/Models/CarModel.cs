using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web.Models
{
    public class CarModel
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public int Mileage { get; set; }
        public DateTime? Reserved { get; set; }
        public LocationModel Location { get; set; }
    }
}

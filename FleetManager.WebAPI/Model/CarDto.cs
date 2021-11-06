using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.WebAPI.Model
{
    public class CarDto : BaseDto
    {
        public string Brand { get; set; }
        public int Mileage { get; set; }
        public DateTime? Reserved { get; set; }
        public string LocationHref { get; set; }


    }
}

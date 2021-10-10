using System;

namespace FleetManager.Entities
{
    public class Car : EntityBase
    {
        public string Brand { get; set; }
        public int Mileage { get; set; }
        public DateTime? Reserved { get; set; }
        public Location Location { get; set; }
    }
}

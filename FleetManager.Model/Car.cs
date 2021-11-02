using System;

namespace FleetManager.Model
{
    public class Car
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public int Mileage { get; set; }
        public DateTime? Reserved { get; set; }
        public Location Location { get; set; }
    }
}

using FleetManager.Model;
using System;

namespace FleetManager.DataAccessLayer.Daos.SqlServer.Entities
{
    class CarEntity 
    {
        public CarEntity()
        {

        }

        public CarEntity(Car model)
        {
            Id = model.Id;
            Brand = model.Brand;
            Mileage = model.Mileage;
            Reserved = model.Reserved;
            LocationId = model.Location?.Id;
        }

        public int? Id { get; set; }
        public string Brand { get; set; }
        public int Mileage { get; set; }
        public DateTime? Reserved { get; set; }
        public int? LocationId { get; set; }
    }
}

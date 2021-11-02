using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos.SqlServer.Entities
{
    class CarEntity : EntityBase
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

        public string Brand { get; set; }
        public int Mileage { get; set; }
        public DateTime? Reserved { get; set; }
        public int? LocationId { get; set; }
    }
}

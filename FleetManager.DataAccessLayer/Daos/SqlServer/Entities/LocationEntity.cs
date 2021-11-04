using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos.SqlServer.Entities
{
    class LocationEntity
    {
        public LocationEntity()
        {

        }

        public LocationEntity(Location model)
        {
            Id = model.Id;
            Name = model.Name;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.DataAccessLayer.Daos.SqlServer.Entities
{
    abstract class EntityBase
    {
        public int? Id { get; set; }
    }
}

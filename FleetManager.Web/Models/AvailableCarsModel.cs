using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web.Models
{
    public class AvailableCarsModel
    {
        public AvailableCarsModel()
        {
            Cars = new List<CarModel>();
        }

        public IList<CarModel> Cars { get; }
    }
}

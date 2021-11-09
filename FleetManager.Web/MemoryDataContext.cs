using FleetManager.DataAccessLayer;
using FleetManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web
{
    public class MemoryDataContext : IDataContext<Tuple<IList<Car>, IList<Location>>>
    {
        public SupportedContextTypes SupportedContext => SupportedContextTypes.Memory;

        private readonly List<Location> _locations;
        private readonly List<Car> _cars;
        private readonly Tuple<IList<Car>, IList<Location>> _data;

        public MemoryDataContext()
        {
            _locations = new List<Location>();
            _cars = new List<Car>();
            _data = new(_cars, _locations);
        }

        public List<Location> Locations => _locations;

        public List<Car> Cars => _cars;

        public Tuple<IList<Car>, IList<Location>> Open()
        {
            return _data;
        }
    }
}

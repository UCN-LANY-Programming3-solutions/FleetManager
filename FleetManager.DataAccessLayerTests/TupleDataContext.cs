using FleetManager.Model;
using System;
using System.Collections.Generic;

namespace FleetManager.DataAccessLayer.Tests
{
    class TupleDataContext : IDataContext<Tuple<IList<Car>, IList<Location>>>
    {
        private readonly IList<Location> _locations;
        private readonly IList<Car> _cars;
        private readonly Tuple<IList<Car>, IList<Location>> _data;

        public TupleDataContext()
        {
            _locations = new List<Location>
                {
                    new Location{ Id = 1, Name = "Aalborg"},
                    new Location{ Id = 2, Name = "Randers"}
                };
            _cars = new List<Car>
                {
                    new Car { Id = 1, Brand = "Ford", Mileage = 12398, Location = _locations[0], Reserved = null },
                    new Car { Id = 2, Brand = "Skoda", Mileage = 5466, Location = _locations[1], Reserved = null },
                };
            _data = new(_cars, _locations);
        }


        internal static IDataContext Create()
        {
            return new TupleDataContext();
        }

        public Tuple<IList<Car>, IList<Location>> Open()
        {
            return _data;
        }
    }
}

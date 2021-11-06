using FleetManager.DataAccessLayer;
using FleetManager.Model;
using FleetManager.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IDao<Location> _locationsDao;

        public LocationsController(IDao<Location> locationsDao)
        {
            _locationsDao = locationsDao;
        }

        [HttpGet]
        public IEnumerable<LocationDto> Get()
        {
            foreach (Location location in _locationsDao.Read())
            {
                yield return location.Map();
            }
        }

        [HttpGet("{id}")]
        public LocationDto Get(int id)
        {
            return _locationsDao.Read(l =>l.Id == id).Single().Map();
        }

        [HttpPost]
        public void Post(LocationDto locationDto)
        {
            Location location =  _locationsDao.Create(locationDto.Map());

            if (location.Id.HasValue)
            {
                Response.StatusCode = StatusCodes.Status201Created;
                Response.Headers["Location"] = @$"/api/locations/{location.Id}";
            }
            else
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, LocationDto locationDto)
        {
            Location locationEntity = locationDto.Map();
            locationEntity.Id = id;
            _locationsDao.Update(locationEntity);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Location location = _locationsDao.Read(l => l.Id == id).Single();
            _locationsDao.Delete(location);
        }
    }
}

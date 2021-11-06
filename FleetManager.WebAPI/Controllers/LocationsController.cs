using FleetManager.DataAccessLayer;
using FleetManager.Entities;
using FleetManager.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            foreach (Location location in _locationsDao.ReadAll())
            {
                yield return location.Map();
            }
        }

        [HttpGet("{id}")]
        public LocationDto Get(int id)
        {
            return _locationsDao.ReadById(id).Map();
        }

        [HttpPost]
        public void Post(LocationDto locationDto)
        {
            int id = _locationsDao.Create(locationDto.Map());
            if (id < 0)
            {
                Response.StatusCode = StatusCodes.Status201Created;
                Response.Headers["Location"] = @$"/api/locations/{id}";
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
            Location location = _locationsDao.ReadById(id);
            _locationsDao.Delete(location);
        }
    }
}

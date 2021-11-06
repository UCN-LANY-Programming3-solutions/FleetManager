using FleetManager.DataAccessLayer;
using FleetManager.Entities;
using FleetManager.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IDao<Car> _carDao;
        private readonly IDao<Location> _locationDao;

        public CarsController(IDao<Car> carDao, IDao<Location> locationDao)
        {
            _carDao = carDao;
            _locationDao = locationDao;
        }

        // GET api/cars
        [HttpGet]
        public IEnumerable<CarDto> Get()
        {
            foreach (Car car in _carDao.ReadAll())
            {
                yield return car.Map();
            }
        }

        // GET api/cars/5
        [HttpGet("{id}")]
        public CarDto Get(int id)
        {
            return _carDao.ReadById(id).Map();
        }

        // POST api/cars
        [HttpPost]
        public void Post(CarDto dto)
        {
            Car car = dto.Map();
            int? locationId = DtoEntityHelpers.GetIdFromHref(dto.LocationHref);
            if (locationId.HasValue)
            {
                car.Location = _locationDao.ReadById(locationId.Value);
            }
            int id = _carDao.Create(car);

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

        // PUT api/cars/5
        [HttpPut("{id}")]
        public void Put(int id, CarDto dto)
        {
            Car car = dto.Map();
            car.Id = id;
            _carDao.Update(car);
        }

        // DELETE api/cars/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Car car = _carDao.ReadById(id);
            _carDao.Delete(car);
        }
    }
}

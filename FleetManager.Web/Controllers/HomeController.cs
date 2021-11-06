using FleetManager.DataAccessLayer;
using FleetManager.Model;
using FleetManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace FleetManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDao<Car> _carDao;

        public HomeController(ILogger<HomeController> logger, IDao<Car> carDao)
        {
            _logger = logger;
            _carDao = carDao;
        }

        public IActionResult Index()
        {
            AvailableCarsModel model = new();

            foreach (Car car in _carDao.Read())
            {
                if (car.Reserved == null)
                {
                    model.Cars.Add(new CarModel
                    {
                        Id = car.Id,
                        Brand = car.Brand,
                        Location = car.Location != null ? new LocationModel
                        {
                            Name = car.Location.Name,
                        } : null
                    });
                }
            }
            return View(model);
        }

        [HttpGet("/home/details/{id}")]
        public IActionResult Details(int id)
        {
            Car car = _carDao.Read(c => c.Id == id).Single();

            CarModel model = new CarModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Mileage = car.Mileage,
                Reserved = car.Reserved,
                Location = car.Location != null ? new LocationModel
                {
                    Id = car.Location.Id,
                    Name = car.Location.Name
                } : null
            };
            return View(model);
        }

        [HttpPost("/home/details/{id}")]
        public IActionResult Reserve(int id)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

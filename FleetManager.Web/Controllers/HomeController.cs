using FleetManager.DataAccessLayer;
using FleetManager.Model;
using FleetManager.Web.Models;
using FleetManager.Web.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDao<Car> _carDao;
        private readonly IDao<Location> _locationDao;

        public HomeController(IDao<Car> carDao, IDao<Location> locationDao)
        {
            _carDao = carDao;
            _locationDao = locationDao;
        }

        public IActionResult Index()
        {
            IndexModel dto = new()
            {
                AvailableCars = _carDao.Read(c => !c.Reserved.HasValue).Map(),
                AllLocations = _locationDao.Read().Map()
            };
            return View(dto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

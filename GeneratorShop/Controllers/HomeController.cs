using GeneratorShop.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGeneratorRepository _generatorRepository;

        public HomeController(ILogger<HomeController> logger, IGeneratorRepository generatorRepository)
        {
            _logger = logger;
            _generatorRepository = generatorRepository;
        }

        public IActionResult Index()
        {
            var generators=_generatorRepository.GetAll();
            return View(generators);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Goods()
        {
            var generators = _generatorRepository.GetAll();
            return View(generators);
        }
        public IActionResult Basket()
        {
            return View();
        }
        public IActionResult homepage()
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

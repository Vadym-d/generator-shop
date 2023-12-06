using Core.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace GeneratorShop.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGeneratorRepository _generatorRepository;
        private readonly ILogger<GoodsController> _logger;
        private readonly IGeneratorCategoryRepository _generatorCategoryRepository;


        public GoodsController(ILogger<GoodsController> logger, IGeneratorRepository generatorRepository, IGeneratorCategoryRepository generatorCategoryRepository)
        {
            _logger = logger;
            _generatorRepository = generatorRepository; 
            _generatorCategoryRepository = generatorCategoryRepository;
        }


        public IActionResult Goods(decimal? minPrice, decimal? maxPrice, string searchText)
        {
            IEnumerable<Generator> generators;

            if (!string.IsNullOrEmpty(searchText))
            {
                generators = _generatorRepository.GetAll()
                    .Where(g =>
                        (minPrice == null || (decimal)g.Price >= minPrice) &&
                        (maxPrice == null || (decimal)g.Price <= maxPrice) &&
(g.Name.Contains(searchText) ||
 (g.Price.ToString().Contains(searchText)) ||
 (g.PowerOutput.ToString().Contains(searchText)) ||
 (g.FuelConsuming.ToString().Contains(searchText)) ||
 (g.Tank.ToString().Contains(searchText)) ||
 (g.Weight.ToString().Contains(searchText)) ||
 (g.Power.ToString().Contains(searchText)) ))
                    .ToList();
            }

            else
            {
                generators = _generatorRepository.GetAll()
                    .Where(g =>
                        (minPrice == null || (decimal)g.Price >= minPrice) &&
                        (maxPrice == null || (decimal)g.Price <= maxPrice)
                    )
                    .ToList();
            }

            ViewBag.UniquePowers = generators.Select(g => g.Power).Distinct().ToList();
            ViewBag.UniquePowerOutputs = generators.Select(g => g.PowerOutput).Distinct().ToList();
            ViewBag.UniqueFuelConsumings = generators.Select(g => g.FuelConsuming).Distinct().ToList();
            ViewBag.UniqueTanks = generators.Select(g => g.Tank).Distinct().ToList();
            ViewBag.UniqueWeights = generators.Select(g => g.Weight).Distinct().ToList();
            ViewBag.UniqueCategories = generators.Select(g => g.GenratorCategory?.Category).Distinct().ToList();




            ViewBag.MinPrice = generators.Min(g => g.Price);
            ViewBag.MaxPrice = generators.Max(g => g.Price);

            return View(generators);
        }

        public IActionResult ProductDetails(int id)
        {
            var generator = _generatorRepository.FindById(id);
            generator.GenratorCategory = _generatorCategoryRepository.FindById(generator.GenratorCategoryId);
          
            if (generator == null)
            {
                
                return NotFound();
            }

            return View(generator);
        }
    }
}

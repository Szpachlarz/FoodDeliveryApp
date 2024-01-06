using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FoodDeliveryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await _userRepository.GetAllCities();
            ViewData["Cities"] = new SelectList(cities, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> RedirectToCity(int id)
        {
            var selectedCity = await _userRepository.GetCityById(id);

            if (selectedCity != null)
            {
                return RedirectToAction("City", new { id = selectedCity.Id });
            }

            return RedirectToAction("Index");
        }

        //[Route("City/{id:int}")]
        public async Task <IActionResult> City(int id)
        {
            var restaurants = await _userRepository.GetRestaurantsByCity(id);
            return View(restaurants);
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
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Services;
using FoodDeliveryApp.Utils;
using FoodDeliveryApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodDeliveryApp.Controllers
{
    public class RestaurantController : Controller
    {
        //public readonly AppDbContext _context;
        public readonly IDishRepository _dishRepository;
        public readonly IDishCategoryRepository _dishCategoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public RestaurantController(IDishRepository dishRepository,  IHttpContextAccessor httpContextAccessor, IDishCategoryRepository dishCategoryRepository, IPhotoService photoService)
        {
            _dishRepository = dishRepository;
            _httpContextAccessor = httpContextAccessor;
            _dishCategoryRepository = dishCategoryRepository;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("restaurant"))
            {
                return RedirectToAction("Index", "Home");
            }

            var restaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();

            var dishCount = await _dishRepository.GetCountAsync(restaurantID);

            var restaurantIndex = new RestaurantIndexViewModel
            {
                DishCount = dishCount,
            };

            return View(restaurantIndex);
        }

        [HttpGet]
        public async Task<IActionResult> DishList(int pg = 1)
        {
            if (!User.IsInRole("restaurant"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var restaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();
                var dishes = await _dishRepository.GetAll(restaurantID);
                List<DishCategory> dishCategories = new List<DishCategory>();

                //const int pageSize = 10;
                //if (pg < 1)
                //    pg = 1;
                //int recsCount = dishes.Count();
                //var pager = new Pager(recsCount, pg, pageSize);
                //int recSkip = (pg - 1) * pageSize;
                //var data = dishes.Skip(recSkip).Take(pager.PageSize).ToList();
                //this.ViewBag.Pager = pager;

                var dishListVM = new DishListViewModel
                {
                    Dishes = dishes,
                };

                return View(dishListVM);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var curRestaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var categories = await _dishCategoryRepository.GetAll(curRestaurantID);
            ViewData["DishCategories"] = new SelectList((System.Collections.IEnumerable)categories, "Id", "Name");
            var addDishViewModel = new AddDishViewModel { AppUserId = curRestaurantID };
            return View(addDishViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDishViewModel dishVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(dishVM.Image);

                var dish = new Dish
                {
                    Id = dishVM.Id,
                    Name = dishVM.Name,
                    Ingredients = dishVM.Ingredients,
                    Price = dishVM.Price,
                    DishCategoryId = dishVM.DishCategoryId,
                    Image = result.Url.ToString(),
                    RestaurantId = dishVM.AppUserId
                };
                _dishRepository.Add(dish);
                return RedirectToAction("DishList");
            }
            else
            {
                ModelState.AddModelError("", "fail");
            }

            return View(dishVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curRestaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var dish = await _dishRepository.GetByIdAsync(id);
            if (dish == null) return View("Error");
            var categories = await _dishCategoryRepository.GetAll(curRestaurantID);
            ViewData["DishCategories"] = new SelectList((System.Collections.IEnumerable)categories, "Id", "Name");
            var dishVM = new EditDishViewModel
            {
                Id = id,
                Name = dish.Name,
                Ingredients = dish.Ingredients,
                Price = dish.Price,
                DishCategoryId = dish.DishCategoryId,
                URL = dish.Image,
                AppUserId = curRestaurantID
            };
            return View(dishVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDishViewModel dishVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View(dishVM);
            }

            var dishResult = await _dishRepository.GetByIdAsyncNoTracking(id);

            if (dishResult == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(dishVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(dishVM);
            }

            if (!string.IsNullOrEmpty(dishResult.Image))
            {
                _ = _photoService.DeletePhotoAsync(dishResult.Image);
            }

            var dish = new Dish
            {
                Id = dishVM.Id,
                Name = dishVM.Name,
                Ingredients = dishVM.Ingredients,
                DishCategoryId = dishVM.DishCategoryId,
                Image = photoResult.Url.ToString(),
                RestaurantId = dishVM.AppUserId
            };

            _dishRepository.Update(dish);

            return RedirectToAction("DishList");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _dishRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dishDetails = await _dishRepository.GetByIdAsync(id);

            if (dishDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(dishDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(dishDetails.Image);
            }

            _dishRepository.Delete(dishDetails);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DishCategoryList(int pg = 1)
        {
            if (!User.IsInRole("restaurant"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var restaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();
                var categories = await _dishCategoryRepository.GetAll(restaurantID);

                const int pageSize = 10;
                if (pg < 1)
                    pg = 1;
                int recsCount = categories.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = categories.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;

                var dishCategoryListVM = new DishCategoryListViewModel
                {
                    Categories = data
                };

                //return View(authors);
                return View(dishCategoryListVM);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddDishCategory()
        {
            var curRestaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var addDishCategoryViewModel = new AddDishCategoryViewModel { AppUserId = curRestaurantID };
            return View(addDishCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDishCategory(AddDishCategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var category = new DishCategory
                {
                    Id = categoryVM.Id,
                    Name = categoryVM.Name,
                    RestaurantId = categoryVM.AppUserId
                };
                _dishCategoryRepository.Add(category);
                return RedirectToAction("DishCategoryList");
            }
            else
            {
                ModelState.AddModelError("", "fail");
            }

            return View(categoryVM);

        }

        [HttpGet]
        public async Task<IActionResult> EditDishCategory(int id)
        {
            var curRestaurantID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var category = await _dishCategoryRepository.GetByIdAsync(id);
            if (category == null) return View("Error");
            var categoryVM = new EditDishCategoryViewModel
            {
                Id = id,
                Name = category.Name,
                AppUserId = curRestaurantID
            };
            return View(categoryVM);

        }

        [HttpPost]
        public async Task<IActionResult> EditDishCategory(EditDishCategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View(categoryVM);
            }

            var category = new DishCategory
            {
                Id = categoryVM.Id,
                Name = categoryVM.Name,
                RestaurantId = categoryVM.AppUserId
            };

            _dishCategoryRepository.Update(category);

            return RedirectToAction("DishCategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDishCategory(int id)
        {
            var category = await _dishCategoryRepository.GetByIdAsync(id);
            if (category == null) return View("Error");
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDishCategory2(int id)
        {
            var category = await _dishCategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return View("Error");
            }

            _dishCategoryRepository.Delete(category);
            return RedirectToAction("Index");
        }


    }

}

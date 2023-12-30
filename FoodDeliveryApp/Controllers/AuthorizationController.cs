using FoodDeliveryApp.Data.Enum;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.ViewModels;

namespace FoodDeliveryApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly UserManager<Restaurant> _restaurantManager;
        //private readonly SignInManager<Restaurant> _signInRestaurantManager;
        private readonly AppDbContext _context;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, /*UserManager<Restaurant> restaurantManager, SignInManager<Restaurant> signInRestaurantManager,*/ AppDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            //_restaurantManager = restaurantManager;
            //_signInRestaurantManager = signInRestaurantManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (roles.Contains("restaurant"))
                        {
                            return RedirectToAction("Index", "Restaurant");
                        }
                        else
                        {
                            return RedirectToAction("Index", "User");
                        }

                    }
                }
                TempData["Error"] = "Błędne dane logowania";
                return View(loginViewModel);
            }
            TempData["Error"] = "Błędne dane logowania";
            return View(loginViewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> LoginRestaurant(LoginViewModel loginViewModel)
        //{
        //    if (!ModelState.IsValid) return View(loginViewModel);

        //    var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

        //    if (user != null)
        //    {
        //        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
        //        if (passwordCheck)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
        //            if (result.Succeeded)
        //            {
        //                var role = await _userManager.GetRolesAsync(user);
        //                HttpContext.Session.SetString(Utils.Const.LOGGED_USER, user.UserName);
        //                HttpContext.Session.SetString(Utils.Const.USER_ID, user.Id);
        //                HttpContext.Session.SetString(Utils.Const.USER_ROLE, role[0]);
        //                HttpContext.Session.SetInt32(Utils.Const.USER_STATUS, user.Status);
        //                if (role[0] == "admin")
        //                {
        //                    return RedirectToAction("Index", "Admin");
        //                }
        //                //else if (role[0] == "restaurant")
        //                //{
        //                //    return RedirectToAction("Index", "Restaurant");
        //                //}
        //                else
        //                {
        //                    return RedirectToAction("Index", "User");
        //                }

        //            }
        //        }
        //        TempData["Error"] = "Błędne dane logowania";
        //        return View(loginViewModel);
        //    }
        //    TempData["Error"] = "Błędne dane logowania";
        //    return View(loginViewModel);
        //}

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        public IActionResult RegisterRestaurant()
        {
            var response = new RegisterRestaurantViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var useremail = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (useremail != null)
            {
                TempData["Error"] = "Ten adres e-mail już znajduje się w bazie danych";
                return View(registerViewModel);
            }

            var username = await _userManager.FindByNameAsync(registerViewModel.Username);
            if (username != null)
            {
                TempData["Error"] = "Ta nazwa użytkownika jest już zajęta";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.Username,
                Status = 0

            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RestaurantRegister(RegisterRestaurantViewModel registerRestaurantViewModel)
        {
            if (!ModelState.IsValid) return View(registerRestaurantViewModel);

            var useremail = await _userManager.FindByEmailAsync(registerRestaurantViewModel.EmailAddress);
            if (useremail != null)
            {
                TempData["Error"] = "Ten adres e-mail już znajduje się w bazie danych";
                return View(registerRestaurantViewModel);
            }

            var username = await _userManager.FindByNameAsync(registerRestaurantViewModel.Username);
            if (username != null)
            {
                TempData["Error"] = "Ta nazwa restauracji jest już zajęta";
                return View(registerRestaurantViewModel);
            }

            var newRestaurant = new User()
            {
                Email = registerRestaurantViewModel.EmailAddress,
                UserName = registerRestaurantViewModel.Username,
                RestaurantName = registerRestaurantViewModel.RestaurantName,
                Description = registerRestaurantViewModel.Description
            };

            var newRestaurantResponse = await _userManager.CreateAsync(newRestaurant, registerRestaurantViewModel.Password);

            if (newRestaurantResponse.Succeeded)
                await _userManager.AddToRoleAsync(newRestaurant, UserRoles.Restaurant);

            return RedirectToAction("Index", "Home");
        }
    }
}

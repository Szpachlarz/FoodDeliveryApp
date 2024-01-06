using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ICartRepository cartRepository, IDishRepository dishRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _dishRepository = dishRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> GetUserCart()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var cart = await _cartRepository.GetUserCart(userId);
            return View(cart);
        }

        public async Task<IActionResult> AddItem(int itemId, int qty = 1, int redirect = 0)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var cart = await _cartRepository.GetCart(userId);
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                };
                _cartRepository.NewShoppingCart(cart);
            }

            var cartItem = await _cartRepository.GetCartItem(cart.Id, itemId);

            if (cartItem != null)
            {
                cartItem.Quantity += qty;
            }
            else
            {
                var dish = await _dishRepository.GetByIdAsync(itemId);
                cartItem = new ShoppingCartItem
                {
                    DishId = itemId,
                    ShoppingCartId = cart.Id,
                    Quantity = qty,
                    Price = dish.Price
                };
                _cartRepository.AddItem(cartItem);
            }
            if (redirect == 0)
                return Ok(cartItem);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var cart = await _cartRepository.GetCart(userId);
            if (cart == null)
            {
                return View("Error");
            }

            var cartItem = await _cartRepository.GetCartItem(cart.Id, itemId);

            if (cartItem == null)
            {
                return View("Error");
            }
            else
            {
                _cartRepository.RemoveItem(cartItem);
            }

            return RedirectToAction("GetUserCart");
        }

    }
}

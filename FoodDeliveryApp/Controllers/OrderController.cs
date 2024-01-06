using FoodDeliveryApp.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> UserOrders()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var orders = await _orderRepository.GetUserOrders(userId);
            return View(orders);
        }
    }
}

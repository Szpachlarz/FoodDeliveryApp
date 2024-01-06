using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userId)
        {
            var orders = await _context.Orders
                            .Include(x => x.OrderItems)
                            .ThenInclude(x => x.Dish)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
            return orders;
        }
    }
}

using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetUserOrders(string userId);
    }
}

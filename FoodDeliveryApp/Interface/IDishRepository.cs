using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Interface
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAll(string restaurantId);

        Task<Dish?> GetByIdAsync(int id);

        bool Add(Dish dish);

        bool Update(Dish dish);

        bool Delete(Dish dish);

        bool Save();

        Task<int> GetCountAsync(string restaurantId);
    }
}

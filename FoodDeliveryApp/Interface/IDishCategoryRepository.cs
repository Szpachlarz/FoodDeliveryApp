using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Interface
{
    public interface IDishCategoryRepository
    {
        Task<IEnumerable<DishCategory>> GetAll(string restaurantId);

        Task<DishCategory?> GetByIdAsync(int id);

        bool Add(DishCategory category);

        bool Update(DishCategory category);

        bool Delete(DishCategory category);

        bool Save();

        Task<int> GetCountAsync(string restaurantId);

        Task<DishCategory?> GetByDishId(int id);
    }
}

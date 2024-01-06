using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<City>> GetAllCities();
        Task<City> GetCityById(int cityId);
        Task<IEnumerable<User>> GetRestaurantsByCity(int cityId);
    }
}

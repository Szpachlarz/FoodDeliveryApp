using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryApp.Models
{
    public class Restaurant
    {
        //public int Id { get; set; }
        public string RestaurantName { get; set; }
        public ICollection<Dish> Dishes { get; set; }
        public string Description { get; set; }
    }
}

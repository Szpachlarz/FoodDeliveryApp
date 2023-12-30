using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class DishCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; } = new List<Dish>();
        [ForeignKey("User")]
        public string RestaurantId { get; set; }
        public User Restaurant { get; set; } = null!;
    }
}

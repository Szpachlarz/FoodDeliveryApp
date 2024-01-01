using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string? Image { get; set; }
        [ForeignKey("User")]
        public string RestaurantId { get; set; }
        public User Restaurant { get; set; } = null!;
        [ForeignKey("DishCategory")]
        public int? DishCategoryId { get; set; }
        public DishCategory? DishCategory { get; set; }
    }
}

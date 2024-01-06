using FoodDeliveryApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.ViewModels
{
    public class AddDishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public double Price { get; set; }
        public string AppUserId { get; set; }
        public IFormFile Image { get; set; }

        [ForeignKey("DishCategory")]
        public int? DishCategoryId { get; set; }
    }
}

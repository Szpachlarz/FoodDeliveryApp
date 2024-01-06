using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.ViewModels
{
    public class EditDishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public double Price { get; set; }
        public string? URL { get; set; }
        public IFormFile Image { get; set; }
        public string AppUserId { get; set; }

        [ForeignKey("DishCategory")]
        public int? DishCategoryId { get; set; }
    }
}

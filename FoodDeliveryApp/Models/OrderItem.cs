using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }
        public double Price { get; set; }
        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}

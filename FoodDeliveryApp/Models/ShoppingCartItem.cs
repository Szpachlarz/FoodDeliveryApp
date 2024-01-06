using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }        
        public int Quantity { get; set; }
        public double Price { get; set; }   
        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        [ForeignKey("ShoppingCart")]
        public string ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}

namespace FoodDeliveryApp.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

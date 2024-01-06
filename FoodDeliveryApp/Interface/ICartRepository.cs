using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Interface
{
    public interface ICartRepository
    {
        bool NewShoppingCart(ShoppingCart cart);
        bool AddItem(ShoppingCartItem item);
        bool RemoveItem(ShoppingCartItem item);
        Task<ShoppingCart> GetUserCart(string userId);
        //Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<ShoppingCartItem> GetCartItem(string cartId, int dishId);
        //Task<bool> DoCheckout();
        bool Save();
    }
}

using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodDeliveryApp.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool NewShoppingCart(ShoppingCart cart)
        {
            _context.Add(cart);
            return Save();
        }

        public bool AddItem(ShoppingCartItem item)
        {
            _context.Add(item);
            return Save();
        }

        public bool RemoveItem(ShoppingCartItem item)
        {
            if (item.Quantity == 1)
                _context.ShoppingCartItems.Remove(item);
            else
                item.Quantity = item.Quantity - 1;
            return Save();
        }

        public async Task<ShoppingCart> GetUserCart(string userId)
        {
            var shoppingCart = await _context.ShoppingCarts
                                  .Include(a => a.ShoppingCartItems)
                                  .ThenInclude(a => a.Dish)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;

        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public async Task<ShoppingCartItem> GetCartItem(string cartId, int dishId)
        {
            var cartItem = _context.ShoppingCartItems
                                  .FirstOrDefault(a => a.ShoppingCartId == cartId && a.Dish.Id == dishId);
            return cartItem;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }



    }
}

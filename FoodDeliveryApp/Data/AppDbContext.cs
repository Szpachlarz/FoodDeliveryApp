using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FoodDeliveryApp.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //public DbSet<Restaurant> Restaurants { get; set;}
        //public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set;}
        public DbSet<DishCategory> DishCategories { get; set;}
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<OrderItem> OrderItems { get; set;}
        public DbSet<Address> Addresses { get; set;}
        public DbSet<City> Cities { get; set; }
    }
}

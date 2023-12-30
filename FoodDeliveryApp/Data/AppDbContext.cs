using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    }
}

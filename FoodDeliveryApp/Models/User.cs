using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        public int Status { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        //for restaurent
        public string? RestaurantName { get; set; }
        public ICollection<Dish> Dishes { get; } = new List<Dish>();
        public ICollection<DishCategory> DishCategories { get; } = new List<DishCategory>();
        public string? Description { get; set; }
    }
}

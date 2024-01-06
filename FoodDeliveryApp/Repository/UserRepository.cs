using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FoodDeliveryApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetCityById(int cityId)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<IEnumerable<User>> GetRestaurantsByCity(int cityId)
        {
            var usersInRole = await _userManager.Users
                .Include(u => u.Address)
                    .ThenInclude(a => a.City)
                .Include(u => u.Dishes)
                .Include(a => a.DishCategories)
                .ToListAsync();

            var filteredUsers = usersInRole
                .Where(u => _userManager.IsInRoleAsync(u, "restaurant").Result)
                .Where(u => u.Address.City.Id == cityId)
                .ToList();

            return filteredUsers;
        }
    }
}

using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodDeliveryApp.Repository
{
    public class DishCategoryRepository : IDishCategoryRepository
    {
        private readonly AppDbContext _context;

        public DishCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DishCategory>> GetAll(string restaurantId)
        {
            return await _context.DishCategories
                .Where(category => category.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<DishCategory?> GetByIdAsync(int id)
        {
            return await _context.DishCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Add(DishCategory category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Delete(DishCategory category)
        {
            _context.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(DishCategory category)
        {
            _context.Update(category);
            return Save();
        }

        public async Task<int> GetCountAsync(string restaurantId)
        {
            return await _context.DishCategories
                .Where(category => category.RestaurantId == restaurantId)
                .CountAsync();
        }

        public async Task<DishCategory?> GetByDishId(int id)
        {
            return await _context.DishCategories
                .Include(i => i.Dishes)
                .Where(i => i.Dishes.Any(ba => ba.Id == id))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}

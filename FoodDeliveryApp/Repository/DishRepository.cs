using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Repository
{
    public class DishRepository : IDishRepository
    {
        private readonly AppDbContext _context;

        public DishRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAll(string restaurantId)
        {
            return await _context.Dishes
                .Include(i => i.DishCategory)
                .Where(dish => dish.RestaurantId == restaurantId)                
                .ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Dish?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Dishes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Add(Dish dish)
        {
            _context.Add(dish);
            return Save();
        }

        public bool Delete(Dish dish)
        {
            _context.Remove(dish);
            return Save();
        }        

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Dish dish)
        {
            _context.Update(dish);
            return Save();
        }
        public async Task<int> GetCountAsync(string restaurantId)
        {
            return await _context.Dishes
                .Where(dish => dish.RestaurantId == restaurantId)
                .CountAsync();
        }
    }
}

using EcoMeal1.Database_CodeFirst;
using EcoMeal1.Entities_CodeFirst;

using Microsoft.EntityFrameworkCore;

namespace EcoMeal1.Repositories
{
    public class OrderRepository(EcoMealDbContext context)
    {
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            return await context.Orders.FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task AddAsync(Order order)
        {
            await context.Orders.AddAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            context.Orders.Update(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order is null)
                return;
            context.Orders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

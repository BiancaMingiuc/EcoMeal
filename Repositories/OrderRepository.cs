using EcoMeal1.Database_CodeFirst;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal1.Repositories
{
    public class OrderRepository(EcoMealDbContext context) : IOrderRepository
    {
        public async Task<List<Order>> GetAllAsync()
        {
            return await context.Orders
                .Include(o => o.OrderPackages)
                    .ThenInclude(op => op.Package)
                        .ThenInclude(p => p.Businesses)
                .Include(o => o.Status)
                .Include(o => o.User)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await context.Orders
                .Include(o => o.OrderPackages)
                    .ThenInclude(op => op.Package)
                        .ThenInclude(p => p.Businesses)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByBusinessOwnerIdAsync(string ownerId)
        {
            return await context.Orders
                .Include(o => o.OrderPackages)
                    .ThenInclude(op => op.Package)
                        .ThenInclude(p => p.Businesses)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Where(o => o.Business.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            return await context.Orders
                .Include(o => o.OrderPackages)
                .FirstOrDefaultAsync(o => o.Id == Id);
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

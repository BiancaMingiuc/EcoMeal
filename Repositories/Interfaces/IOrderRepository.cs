using EcoMeal1.Entities_CodeFirst;

namespace EcoMeal1.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAllAsync();
        public Task<List<Order>> GetByUserIdAsync(string userId);
        public Task<List<Order>> GetByBusinessOwnerIdAsync(string ownerId);
        public Task<Order?> GetOrderById(Guid Id);
        public Task AddAsync(Order order);
        public Task DeleteAsync(Guid id);
        public Task SaveChangesAsync();

    }
}

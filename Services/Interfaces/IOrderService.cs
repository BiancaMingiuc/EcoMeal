using EcoMeal1.Entities_CodeFirst;

namespace EcoMeal1.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAllAsync();
        public Task<List<Order>> GetByUserIdAsync(string userId);
        public Task<List<Order>> GetByBusinessOwnerIdAsync(string ownerId);
        public Task CancelAsync(Guid id);
        public Task AddAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, Order updatedOrders);
    }
}

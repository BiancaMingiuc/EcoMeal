using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Repositories;
using EcoMeal1.Repositories.Interfaces;
using EcoMeal1.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace EcoMeal1.Services
{
    public class OrderService (IOrderRepository orderRepository, IPackageRepository packageRepository) : IOrderService
    {
        public async Task<List<Order>> GetAllAsync()
        {
            return await orderRepository.GetAllAsync();
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await orderRepository.GetByUserIdAsync(userId);
        }

        public async Task<List<Order>> GetByBusinessOwnerIdAsync(string ownerId)
        {
            return await orderRepository.GetByBusinessOwnerIdAsync(ownerId);
        }

        public async Task CancelAsync(Guid id, bool restoreQuantity = true)
        {
            var order = await orderRepository.GetOrderById(id);
            if (order != null && (order.StatusId == 1 || order.StatusId == 2))
            {
                order.StatusId = 4;
                
                if (restoreQuantity && order.OrderPackages != null && order.OrderPackages.Any())
                {
                    foreach (var op in order.OrderPackages)
                    {
                        var package = await packageRepository.GetPackageById(op.PackageId);
                        if (package != null)
                        {
                            package.Quantity += op.Quantity;
                        }
                    }
                    await packageRepository.SaveChangesAsync();
                }
                
                await orderRepository.SaveChangesAsync();
            }
        }

        public async Task ConfirmAsync(Guid id)
        {
            var order = await orderRepository.GetOrderById(id);
            if (order != null && order.StatusId == 1)
            {
                order.StatusId = 2; // Confirmed
                await orderRepository.SaveChangesAsync();
            }
        }

        public async Task PickUpAsync(Guid id)
        {
            var order = await orderRepository.GetOrderById(id);
            if (order != null && order.StatusId == 2)
            {
                order.StatusId = 3; // PickedUp
                await orderRepository.SaveChangesAsync();
            }
        }
        public async Task AddAsync(Order order)
        {
            await orderRepository.AddAsync(order);

            if (order.OrderPackages != null && order.OrderPackages.Any())
            {
                foreach (var orderPackage in order.OrderPackages)
                {
                    var package = await packageRepository.GetPackageById(orderPackage.PackageId);
                    
                    if (package != null && package.Quantity >= orderPackage.Quantity)
                    {
                        package.Quantity -= orderPackage.Quantity;
                    }
                    else
                    { 
                        throw new ArgumentException("Insufficient stock.");
                    }
                }
                await packageRepository.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("An order must contain at least one package");
            }

            await orderRepository.SaveChangesAsync();
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await orderRepository.GetOrderById(id);
        }
        public async Task DeleteAsync(Guid id)
        {
            await orderRepository.DeleteAsync(id);
            await orderRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Order updatedOrder)
        {
            var existingOrders = await orderRepository.GetOrderById(id);

            if (existingOrders != null)
            {
                existingOrders.Status = updatedOrder.Status;

                await orderRepository.SaveChangesAsync();
            }
        }
    }
}

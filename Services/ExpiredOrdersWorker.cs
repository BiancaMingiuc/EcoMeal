using EcoMeal1.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EcoMeal1.Services
{
    public class ExpiredOrdersWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredOrdersWorker> _logger;

        public ExpiredOrdersWorker(IServiceScopeFactory scopeFactory, ILogger<ExpiredOrdersWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ExpiredOrdersWorker starting...");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                        var orders = await orderService.GetAllAsync();

                        foreach (var order in orders)
                        {
                            // StatusId 1 = Pending, 2 = Confirmed
                            if (order.StatusId == 1 || order.StatusId == 2)
                            {
                                if (order.OrderPackages != null && order.OrderPackages.Any())
                                {
                                    // Check if any package in the order has expired
                                    bool hasExpiredPackage = order.OrderPackages.Any(op => 
                                        op.Package != null && op.Package.ExpirationDate < DateTime.Now);

                                    if (hasExpiredPackage)
                                    {
                                        _logger.LogInformation($"Canceling expired order {order.Id} without restoring stock.");
                                        // restoreQuantity = false
                                        await orderService.CancelAsync(order.Id, false);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing expired orders.");
                }

                // Check every 1 minute
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}

using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Services;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal1.Controllers
{
    [ApiController]
    [Route("/")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            return await orderService.GetAllAsync();
        }

        public async Task<ActionResult> AddAsync(Order order)
        {
            await orderService.AddAsync(order);
            return Ok();
        }

        public async Task<ActionResult<Order>> GetById(Guid id)
        {
            return await orderService.GetByIdAsync(id);
        }

        public async Task<ActionResult> UpdateAsync(Guid id, Order order)
        {
            await orderService.UpdateAsync(id, order);
            return Ok();
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await orderService.DeleteAsync(id);
            return Ok();
        }
    }
}

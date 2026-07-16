using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Services;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,BusinessManager,Customer")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            return await orderService.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(Order order)
        {
            await orderService.AddAsync(order);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(Guid id)
        {
            return await orderService.GetByIdAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, Order order)
        {
            await orderService.UpdateAsync(id, order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await orderService.DeleteAsync(id);
            return Ok();
        }
    }
}

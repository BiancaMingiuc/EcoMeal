using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal1.Controllers
{
    [ApiController]
    [Route("/")]
    public class BusinessesController(IBusinessesService businessesService) : ControllerBase
    {
        public async Task<ActionResult<List<Businesses>>> GetAll()
        {
            return await businessesService.GetAllAsync();
        }

        public async Task<ActionResult>AddAsync(Businesses business)
        {
            await businessesService.AddAsync(business);
            return Ok();
        }

        public async Task<ActionResult<Businesses>> GetById(Guid id)
        {
            return await businessesService.GetByIdAsync(id);
        }

        public async Task<ActionResult> UpdateAsync(Guid id, Businesses business)
        {
            await businessesService.UpdateAsync(id, business);
            return Ok();
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await businessesService.DeleteAsync(id);
            return Ok();
        }

    }
}

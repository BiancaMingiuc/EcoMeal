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
    public class PackageController(IPackageService packageService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Package>>> GetAll()
        {
            return await packageService.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(Package package)
        {
            await packageService.AddAsync(package);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetById(Guid id)
        {
            return await packageService.GetByIdAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, Package package)
        {
            await packageService.UpdateAsync(id, package);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await packageService.DeleteAsync(id);
            return Ok();
        }

    }
}

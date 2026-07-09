using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Services;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal1.Controllers
{
    public class PackageController(IPackageService packageService) : ControllerBase
    {
        public async Task<ActionResult<List<Package>>> GetAll()
        {
            return await packageService.GetAllAsync();
        }

        public async Task<ActionResult> AddAsync(Package package)
        {
            await packageService.AddAsync(package);
            return Ok();
        }

        public async Task<ActionResult<Package>> GetById(Guid id)
        {
            return await packageService.GetByIdAsync(id);
        }

        public async Task<ActionResult> UpdateAsync(Guid id, Package package)
        {
            await packageService.UpdateAsync(id, package);
            return Ok();
        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await packageService.DeleteAsync(id);
            return Ok();
        }

    }
}

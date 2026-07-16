using EcoMeal1.Database_CodeFirst;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal1.Repositories
{
    public class PackageRepository(EcoMealDbContext context) : IPackageRepository
    {
        public async Task<List<Package>> GetAllAsync()
        {
            return await context.Packages.Include(p => p.Businesses).Include(p => p.PackageType).ToListAsync();
        }

        public async Task<List<Package>> GetByBusinessIdAsync(Guid businessId)
        {
            return await context.Packages
                .Where(p => p.BusinessId == businessId)
                .Include(p => p.PackageType)
                .Include(p => p.Businesses)
                .ToListAsync();
        }

        public async Task<Package> GetPackageById(Guid Id)
        {
            return await context.Packages.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task AddAsync(Package package)
        {
            await context.Packages.AddAsync(package);
        }


        public async Task DeleteAsync(Guid id)
        {
            var pack = await context.Packages.FirstOrDefaultAsync(p => p.Id == id);
            if (pack is null)
                return;
            context.Packages.Remove(pack);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

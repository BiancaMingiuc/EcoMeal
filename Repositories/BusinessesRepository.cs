using EcoMeal1.Database_CodeFirst;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal1.Repositories
{
    public class BusinessesRepository(EcoMealDbContext context) : IBusinessesRepository
    {
        public async Task<List<Businesses>> GetAllAsync()
        {
            return await context.Businesses.ToListAsync();
        }

        public async Task<List<Businesses>> GetAllWithPackagesAsync()
        {
            return await context.Businesses
                .Include(b => b.Packages)
                .Include(b => b.BusinessType)
                .ToListAsync();
        }

        public async Task<Businesses> GetBusinessById(Guid Id)
        {
            return await context.Businesses.FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task AddAsync(Businesses business)
        {
            await context.Businesses.AddAsync(business);
        }

        //public async Task UpdateAsync(Businesses business)
        //{
        //    context.Businesses.Update(business);
        //}

        public async Task DeleteAsync(Guid id)
        {
            var business = await context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
            if (business is null)
                return;
            context.Businesses.Remove(business);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

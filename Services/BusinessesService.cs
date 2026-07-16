using EcoMeal1.Repositories;
using EcoMeal1.Repositories.Interfaces;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Database_CodeFirst;
using EcoMeal1.Services.Interfaces;

namespace EcoMeal1.Services
{
    public class BusinessesService(IBusinessesRepository businessesRepository) : IBusinessesService
    {
        public async Task<List<Businesses>> GetAllAsync()
        {
            return await businessesRepository.GetAllAsync();
        }

        public async Task<List<Businesses>> GetAllWithPackagesAsync()
        {
            return await businessesRepository.GetAllWithPackagesAsync();
        }

        public async Task AddAsync(Businesses business)
        {
            await businessesRepository.AddAsync(business);
            await businessesRepository.SaveChangesAsync();
        }

        public async Task<Businesses> GetByIdAsync(Guid id)
        {
            return await businessesRepository.GetBusinessById(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await businessesRepository.DeleteAsync(id);
            await businessesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Businesses updatedBusiness)
        {
            var existingBusiness = await businessesRepository.GetBusinessById(id);

            if (existingBusiness != null)
            {
                existingBusiness.Name = updatedBusiness.Name;
                existingBusiness.Description = updatedBusiness.Description;
                existingBusiness.Address = updatedBusiness.Address;
                existingBusiness.ImageURL = updatedBusiness.ImageURL;
                existingBusiness.BusinessTypeId = updatedBusiness.BusinessTypeId;

                await businessesRepository.SaveChangesAsync();
            }
        }


    }
}

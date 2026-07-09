using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Repositories;
using EcoMeal1.Repositories.Interfaces;
using EcoMeal1.Services.Interfaces;

namespace EcoMeal1.Services
{
    public class PackageService(IPackageRepository packageRepository) : IPackageService
    {
        public async Task<List<Package>> GetAllAsync()
        {
            return await packageRepository.GetAllAsync();
        }

        public async Task AddAsync(Package package)
        {
            await packageRepository.AddAsync(package);
            await packageRepository.SaveChangesAsync();
        }

        public async Task<Package> GetByIdAsync(Guid id)
        {
            return await packageRepository.GetPackageById(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await packageRepository.DeleteAsync(id);
            await packageRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Package updatedPackage)
        {
            var existingPackage = await packageRepository.GetPackageById(id);

            if (existingPackage != null)
            {
                existingPackage.Name = updatedPackage.Name;
                existingPackage.Description = updatedPackage.Description;
                existingPackage.Price = updatedPackage.Price;
                existingPackage.Quantity = updatedPackage.Quantity;
                existingPackage.PickupStart = updatedPackage.PickupStart;
                existingPackage.PickupEnd = updatedPackage.PickupEnd;
                existingPackage.ImageURL = updatedPackage.ImageURL;
                existingPackage.BusinessId = updatedPackage.BusinessId;
                existingPackage.PackageTypeId = updatedPackage.PackageTypeId;

                await packageRepository.SaveChangesAsync();
            }
        }


    }
}

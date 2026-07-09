using EcoMeal1.Entities_CodeFirst;

namespace EcoMeal1.Services.Interfaces
{
    public interface IPackageService
    {
        public Task<List<Package>> GetAllAsync();
        public Task AddAsync(Package package);
        Task<Package> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, Package updatedPackage);
    }
}

using EcoMeal1.Entities_CodeFirst;

namespace EcoMeal1.Repositories.Interfaces
{
    public interface IPackageRepository
    {
        public Task<List<Package>> GetAllAsync();
        public Task<Package?> GetPackageById(Guid Id);
        public Task AddAsync(Package package);
        public Task DeleteAsync(Guid id);
        public Task SaveChangesAsync();
    }
}

using EcoMeal1.Entities_CodeFirst;

namespace EcoMeal1.Services.Interfaces
{
    public interface IBusinessesService
    {
        public Task<List<Businesses>> GetAllAsync();
        public Task<List<Businesses>> GetAllWithPackagesAsync();
        public Task AddAsync(Businesses business);
        Task<Businesses> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, Businesses updatedBusiness);
        //public Task SaveChangesAsync();
    }
}

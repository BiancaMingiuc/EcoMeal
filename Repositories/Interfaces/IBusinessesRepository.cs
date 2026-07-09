using EcoMeal1.Entities_CodeFirst;

namespace EcoMeal1.Repositories.Interfaces
{
    public interface IBusinessesRepository
    {
        public Task<List<Businesses>> GetAllAsync();
        public Task<Businesses?> GetBusinessById(Guid Id);
        public Task AddAsync(Businesses business);
        public Task DeleteAsync(Guid id);
        public Task SaveChangesAsync();

        //public void UpdateAsync(Businesses business);
    }
}

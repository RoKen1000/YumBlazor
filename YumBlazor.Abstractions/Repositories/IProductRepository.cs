using YumBlazor.Models.Entities;

namespace YumBlazor.Abstractions.Repositories
{
    public interface IProductRepository
    {
        public Task<Product> CreateAsync(Product product);
        public Task<Product> UpdateAsync(Product product);
        public Task<bool> DeleteAsync(int id);
        public Task<Product> GetByIdAsync(int id);
        public Task<IEnumerable<Product>> GetAllAsync();
    }
}

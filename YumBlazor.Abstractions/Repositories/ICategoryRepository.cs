using YumBlazor.Models.Entities;

namespace YumBlazor.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task<bool> DeleteAsync(int id);
        public Task<Category> GetByIdAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
    }
}

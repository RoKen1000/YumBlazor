using Microsoft.EntityFrameworkCore;
using YumBlazor.Abstractions.Repositories;
using YumBlazor.DataAccess;
using YumBlazor.Models.Entities;

namespace YumBlazor.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                _db.Categories.Remove(category);

                return (await _db.SaveChangesAsync()) > 0;
            }

            return false;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(category != null)
            {
                return category;
            }

            return new Category();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryEntity = await _db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (categoryEntity != null)
            {
                categoryEntity.Name = category.Name;

                _db.Categories.Update(categoryEntity);
                await _db.SaveChangesAsync();
                return categoryEntity;
            }

            return category;
        }
    }
}

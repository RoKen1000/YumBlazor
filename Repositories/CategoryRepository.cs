using YumBlazor.Abstractions.Repositories;
using YumBlazor.DataAccess;
using YumBlazor.Models.Entities;

namespace YumBlazor.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();

            return category;
        }

        public bool Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                _db.Categories.Remove(category);

                return _db.SaveChanges() > 0;
            }

            return false;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category GetById(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);

            if(category != null)
            {
                return category;
            }

            return new Category();
        }

        public Category Update(Category category)
        {
            var categoryEntity = _db.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (categoryEntity != null)
            {
                categoryEntity.Name = category.Name;

                _db.Categories.Update(categoryEntity);
                _db.SaveChanges();
                return categoryEntity;
            }

            return category;
        }
    }
}

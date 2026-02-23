using YumBlazor.Models.Entities;

namespace YumBlazor.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        public Category Create(Category category);
        public Category Update(Category category);
        public bool Delete(int id);
        public Category GetById(int id);
        public IEnumerable<Category> GetAll();
    }
}

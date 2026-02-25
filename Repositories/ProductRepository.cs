using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using YumBlazor.Abstractions.Repositories;
using YumBlazor.DataAccess;
using YumBlazor.Models.Entities;

namespace YumBlazor.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            if (product != null)
            {
                _db.Products.Remove(product);

                return (await _db.SaveChangesAsync()) > 0;
            }

            return false;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product != null)
            {
                return product;
            }

            return new Product();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productEntity = await _db.Products.FirstOrDefaultAsync(c => c.Id == product.Id);

            if (productEntity != null)
            {
                productEntity.Name = product.Name;
                productEntity.Description = product.Description;
                productEntity.Price = product.Price;
                productEntity.ImageUrl = product.ImageUrl;
                productEntity.CategoryId = product.CategoryId;

                _db.Products.Update(productEntity);
                await _db.SaveChangesAsync();
                return productEntity;
            }

            return product;
        }
    }
}

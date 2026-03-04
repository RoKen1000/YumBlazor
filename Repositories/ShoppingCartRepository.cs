using Microsoft.EntityFrameworkCore;
using YumBlazor.Abstractions.Repositories;
using YumBlazor.DataAccess;
using YumBlazor.Models.Entities;

namespace YumBlazor.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ClearCartAsync(string? userId)
        {
            var cartItems = await _db.ShoppingCarts.Where(sc => sc.UserId == userId).ToListAsync();
            _db.ShoppingCarts.RemoveRange(cartItems);
            return await _db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Gets all items in a user's shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _db.ShoppingCarts.Where(sc => sc.UserId == userId)
                .Include(a => a.Product)
                .ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == userId && sc.ProductId == productId);

            if(cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };

                await _db.ShoppingCarts.AddAsync(cart);
            }
            else
            {
                cart.Count += updateBy;

                if(cart.Count <= 0)
                {
                    _db.Remove(cart);
                }
            }

            return await _db.SaveChangesAsync() > 0;
        }
    }
}

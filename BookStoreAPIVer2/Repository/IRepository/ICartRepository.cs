using System.Linq.Expressions;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface ICartRepository
{
    Task RemoveItemAsync(Cart cart);
    
    Task<Cart> AddItemAsync(Cart cart);
    
    Task<Cart> UpdateQuantityAsync(Cart cart);

    Task<Cart> GetAsync(Expression<Func<Cart, bool>>? filter = null);

    Task<List<Cart>> GetListAsync(Expression<Func<Cart, bool>>? filter = null);
}
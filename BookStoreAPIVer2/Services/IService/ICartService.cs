using BookStoreAPIVer2.DTOs.Cart;

namespace BookStoreAPIVer2.Services.IService;

public interface ICartService
{
    Task RemoveItemAsync(int cartId);
    
    Task<CartDTO> AddItemAsync(CreateCartDTO cart);
    
    Task<CartDTO> UpdateQuantityAsync(UpdateCartDTO cart);

    Task<List<CartDTO>> GetCartByCustomerId(int customerId);
}
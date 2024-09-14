using AutoMapper;
using BookStoreAPIVer2.DTOs.Cart;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;

namespace BookStoreAPIVer2.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<List<CartDTO>> GetCartByCustomerId(int customerId)
    {
        var cartByCustomerId = await _cartRepository.GetListAsync(c => c.CustomerId == customerId);
        
        var listCartDto = _mapper.Map<List<CartDTO>>(cartByCustomerId);
        
        return listCartDto;
    }

    public async Task RemoveItemAsync(int cartId)
    {
        var cartToRemove = await _cartRepository.GetAsync(c => c.CartId == cartId);
        await _cartRepository.RemoveItemAsync(cartToRemove);
    }

    public async Task<CartDTO> AddItemAsync(CreateCartDTO cart)
    {
        var cartToAdd = _mapper.Map<Cart>(cart);

        var checkCart = await _cartRepository.GetAsync(c => c.BookId == cart.BookId);

        Cart result;

        if (checkCart == null)
        {
            result = await _cartRepository.AddItemAsync(cartToAdd);
        }
        else
        {
            var newCart = new Cart
            {
                CartId = checkCart.CartId,
                Quantity = cart.Quantity + checkCart.Quantity
            };
            result = await _cartRepository.UpdateQuantityAsync(newCart);
        }
        
        var cartToReturn = _mapper.Map<CartDTO>(result);
        
        return cartToReturn;
    }

    public async Task<CartDTO> UpdateQuantityAsync(UpdateCartDTO cart)
    {
        var cartToUpdate = _mapper.Map<Cart>(cart);

        var result = await _cartRepository.UpdateQuantityAsync(cartToUpdate);
        
        var cartToReturn = _mapper.Map<CartDTO>(result);
        
        return cartToReturn;
    }
}
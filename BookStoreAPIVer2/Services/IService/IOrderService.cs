using BookStoreAPIVer2.DTOs;

namespace BookStoreAPIVer2.Services.IService;

public interface IOrderService
{
    Task<List<OrderDetailDTO>> CreateOrderAsync(CreateOrderDTO order, int userId);
}
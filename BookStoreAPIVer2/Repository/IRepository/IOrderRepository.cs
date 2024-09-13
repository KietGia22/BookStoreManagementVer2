using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface IOrderRepository
{
    Task<List<OrderDetail>> CreateOrderAsync(Order order);
}
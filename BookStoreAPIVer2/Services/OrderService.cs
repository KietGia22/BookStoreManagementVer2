using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;

namespace BookStoreAPIVer2.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderDetailDTO>> CreateOrderAsync(CreateOrderDTO orderDto, int userId)
    {
        var order = new OrderDTO
        {
            CustomerId = orderDto.CustomerId,
            AccId = userId,
            CreateDate = DateTime.UtcNow,
            Total = 0
        };
        
        var orderToAdd = _mapper.Map<Order>(order);
        var result = await _orderRepository.CreateOrderAsync(orderToAdd);
        
        var orderDetails = _mapper.Map<List<OrderDetailDTO>>(result);
        return orderDetails;
    }
}
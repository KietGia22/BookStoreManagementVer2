using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPIVer2.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    internal DbSet<Order> _dbSet;

    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
        this._dbSet = _db.Set<Order>();
    }

    public async Task<List<OrderDetail>> CreateOrderAsync(Order order)
    {
        List<Cart> cartOfCustomer = await _db.Carts.Where(c => c.CustomerId == order.CustomerId).ToListAsync();

        var newOrder = await _dbSet.AddAsync(order);
        await _db.SaveChangesAsync();
        
        var bookIds = cartOfCustomer.Select(c => c.BookId).Distinct().ToList();
        var bookPrices = await _db.Books.Where(b => bookIds.Contains(b.BookId)).ToDictionaryAsync(b => b.BookId, b => b.Price);

        List<OrderDetail> orderDetails = cartOfCustomer.Select(cart => new OrderDetail
        {
            InvoiceId = newOrder.Entity.OrderId,
            BookId = cart.BookId,
            Quantity = cart.Quantity,
            Price = cart.Quantity * bookPrices[cart.BookId],
        }).ToList();

        long totalOfOrder = orderDetails.Sum(o => o.Price);

        await UpdateTotalOfOrder(newOrder.Entity, totalOfOrder);
        
        await _db.InvoiceDetails.AddRangeAsync(orderDetails);
        await _db.SaveChangesAsync();
        
        return orderDetails;
    }

    private async Task UpdateTotalOfOrder(Order order, long total)
    {
        order.Total = total;
        
        _db.Entry(order).State = EntityState.Modified;
        
        await _db.SaveChangesAsync();
    }
}
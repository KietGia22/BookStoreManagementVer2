using System.Linq.Expressions;
using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPIVer2.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;
    internal DbSet<Cart> dbSet;

    public CartRepository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = db.Set<Cart>();
    }
    
    public async Task<List<Cart>> GetListAsync(Expression<Func<Cart, bool>>? filter = null)
    {
        IQueryable<Cart> query = dbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        return await query.ToListAsync();
    }

    public async Task<Cart> GetAsync(Expression<Func<Cart, bool>>? filter = null)
    {
        IQueryable<Cart> query = dbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        return await query.FirstOrDefaultAsync();
    }

    public async Task RemoveItemAsync(Cart cart)
    {
        dbSet.Remove(cart);
        await _db.SaveChangesAsync();
    }

    public async Task<Cart> AddItemAsync(Cart cart)
    {
        var cartToDB = await dbSet.AddAsync(cart);

        var bookToUpdate = await _db.Books.FindAsync(cart.BookId);
        
        var newQuantity = bookToUpdate.Quantity - cart.Quantity;

        await UpdateQuantityOfBook(newQuantity, bookToUpdate);
        
        await _db.SaveChangesAsync();
        
        return cartToDB.Entity;
    }

    public async Task<Cart> UpdateQuantityAsync(Cart cart) 
    {
        var cartToUpdate = await dbSet.FindAsync(cart.CartId);

        var bookDTO = await _db.Books.FindAsync(cartToUpdate.BookId);
        
        int newQuantity = bookDTO.Quantity + cartToUpdate.Quantity - cart.Quantity;

        if (newQuantity < 0)
        {
            throw new Exception("Quantity cannot be negative");
        }

        cartToUpdate.Quantity = cart.Quantity;
        
        _db.Entry(cartToUpdate).State = EntityState.Modified;

        await UpdateQuantityOfBook(newQuantity, bookDTO);
        
        await _db.SaveChangesAsync();
        
        return cartToUpdate;
    }

    private async Task UpdateQuantityOfBook(int quantity, Book book)
    {

        book.Quantity = quantity;
        
        _db.Entry(book).State = EntityState.Modified;
        
        await _db.SaveChangesAsync();
    }
}
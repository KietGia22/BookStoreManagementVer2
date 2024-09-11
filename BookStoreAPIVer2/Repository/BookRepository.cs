using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;

namespace BookStoreAPIVer2.Repository;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly ApplicationDbContext _db;

    public BookRepository(ApplicationDbContext db):base(db)
    {
        _db = db;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        var existingBook = await _db.Books.FindAsync(book.BookId);

        if (existingBook != null)
        {
            _db.Entry(existingBook).CurrentValues.SetValues(book);
            await _db.SaveChangesAsync();
        }

        return existingBook;
    }

    public async Task<Image> AddImageAsync(Image image)
    {
        var img = await _db.Images.AddAsync(image);
        await _db.SaveChangesAsync();
        return img.Entity;

    }
}
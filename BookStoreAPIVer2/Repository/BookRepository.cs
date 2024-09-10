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
}
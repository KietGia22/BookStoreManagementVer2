using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface IBookRepository : IRepository<Book>
{
    Task<Book> UpdateAsync(Book book);
    
    Task<Image> AddImageAsync(Image image);
}
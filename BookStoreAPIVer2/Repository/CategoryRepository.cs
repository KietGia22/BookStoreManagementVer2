using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;

namespace BookStoreAPIVer2.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db):base(db)
    {
        _db = db;
    }

    public async Task RemoveAsync(Category category)
    {
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }

}
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    Task RemoveAsync(Category category);
    Task<Category> UpdateAsync(Category category);
}
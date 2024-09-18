using System.Linq.Expressions;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface ITimeRepository
{
    Task<List<TimeKeeping>> GetAllAsync();
    
    Task<TimeKeeping> AddAsync(TimeKeeping entity);
    
    Task UpdateAsync(TimeKeeping entity);

    Task<TimeKeeping> GetAsync(Expression<Func<TimeKeeping, bool>> filter = null);
}
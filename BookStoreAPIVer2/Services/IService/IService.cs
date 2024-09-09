using System.Linq.Expressions;

namespace BookStoreAPIVer2.Services.IService;

public interface IService<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(int cateogryId);
    Task CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    
}
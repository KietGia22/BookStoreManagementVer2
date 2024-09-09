using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Services.IService;

public interface ICategory : IService<CategoryDTO>
{
    Task RemoveAsync(int categoryId);
}
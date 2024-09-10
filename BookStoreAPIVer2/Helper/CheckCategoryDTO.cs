using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Repository.IRepository;

namespace BookStoreAPIVer2.Helper;

public class CheckCategoryDTO
{
    public async static Task CheckCategoryBeforeModify(CategoryDTO category, ICategoryRepository _categoryRepository)
    {
        if (await _categoryRepository.GetAsync(u => u.CategoryName.ToLower() == category.CategoryName.ToLower()) != null)
        {
            throw new Exception($"{category.CategoryName} already exists");
        }
    }
}
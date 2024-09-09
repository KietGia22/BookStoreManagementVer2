using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;

namespace BookStoreAPIVer2.Services;

public class CategoryService : ICategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        var results = await _categoryRepository.GetAllAsync();
        var categories = _mapper.Map<List<CategoryDTO>>(results);
        return categories;
    }

    public async Task<CategoryDTO> GetAsync(int categoryId)
    {
        var category = await _categoryRepository.GetAsync(c => c.CategoryId == categoryId);
        var result = _mapper.Map<CategoryDTO>(category);
        return result;
    }

    public async Task CreateAsync(CategoryDTO category)
    {
        if (await _categoryRepository.GetAsync(u => u.CategoryName.ToLower() == category.CategoryName.ToLower()) != null)
        {
            throw new Exception($"Category {category.CategoryName} already exists");
        }
        
        var categoryToCreate = _mapper.Map<Category>(category);

        await _categoryRepository.CreateAsync(categoryToCreate);
    }

    public async Task<CategoryDTO> UpdateAsync(CategoryDTO category)
    {
        if (await _categoryRepository.GetAsync(u => u.CategoryName.ToLower() == category.CategoryName.ToLower()) != null)
        {
            throw new Exception($"Category {category.CategoryName} already exists");
        }
        
        var categoryToUpdate = _mapper.Map<Category>(category);

        var categoryReturn = await _categoryRepository.UpdateAsync(categoryToUpdate);
        var result = _mapper.Map<CategoryDTO>(categoryReturn);
        return result;
    }

    public async Task RemoveAsync(int categoryId)
    {
        var category = await _categoryRepository.GetAsync(u => u.CategoryId == categoryId);
        await _categoryRepository.RemoveAsync(category);
    }
}
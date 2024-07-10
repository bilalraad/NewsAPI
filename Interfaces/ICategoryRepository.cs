using NewsAPI.DTOs;
using NewsAPI.Entities;

namespace NewsAPI.Interfaces;

public interface ICategoryRepository
{

    public Task<ICollection<CategoryDto>> GetCategoriesAsync();

    public Task<CategoryDto> GetCategoryByIdAsync(Guid id);

    public Task CreateCategoryAsync(CategoryDto category);

    public Task<Category> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto category);

    public Task DeleteCategoryAsync(Guid id);


}

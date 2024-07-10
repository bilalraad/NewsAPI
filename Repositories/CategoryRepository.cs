using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Errors;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories;

public class CategoryRepository(Context _context, IMapper _mapper) : ICategoryRepository
{
    public async Task CreateCategoryAsync(CategoryDto category)
    {
        var newCategory = new Category
        {
            Name = category.Name,
            Image = category.Image
        };
        await _context.Categories.AddAsync(newCategory);

    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) throw AppException.NotFound("Category not found");

        _context.Categories.Remove(category);
    }

    public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
        .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
        return categories;
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await _context.Categories
        .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) throw AppException.NotFound("Category not found");

        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto category)
    {
        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        if (categoryToUpdate == null) throw AppException.NotFound("Category not found");

        _mapper.Map(category, categoryToUpdate);

        return categoryToUpdate;
    }
}

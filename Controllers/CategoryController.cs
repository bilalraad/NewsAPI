using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Repositories;

namespace NewsAPI.Controllers;

public class CategoryController(UnitOfWork _unitOfWork) : BaseController
{


    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
    {
        await _unitOfWork.CategoryRepository.CreateCategoryAsync(category);
        await _unitOfWork.Complete();
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto category)
    {

        await _unitOfWork.CategoryRepository.UpdateCategoryAsync(id, category);
        await _unitOfWork.Complete();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {

        await _unitOfWork.CategoryRepository.DeleteCategoryAsync(id);
        await _unitOfWork.Complete();
        return NoContent();
    }
}

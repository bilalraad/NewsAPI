using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;

namespace NewsAPI.Controllers;

[Authorize]
public class NewsController(IUnitOfWork _unitOfWork) : IController
{


    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<NewsDto>>> GetAll([FromQuery] NewsFilter newsFilter)
    {
        return Ok(await _unitOfWork.NewsRepository.GetAllNewsAsync(newsFilter));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ServiceFilter(typeof(UpdateNewsViewCount))]
    public async Task<ActionResult<NewsDto>> GetById(Guid id)
    {

        return Ok(await _unitOfWork.NewsRepository.GetNewsByIdAsync(id));
    }

    [HttpPost]
    [Authorize(policy: AppPolicy.RequireModeratorRole)]
    public async Task<ActionResult> Create(CreateNewsDto newsDto)
    {

        await _unitOfWork.NewsRepository.AddNewsAsync(newsDto);
        await _unitOfWork.Complete();
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(policy: AppPolicy.RequireModeratorRole)]
    public async Task<ActionResult> Update(Guid id, UpdateNewsDto updatedNewsDto)
    {

        await _unitOfWork.NewsRepository.UpdateNewsAsync(id, updatedNewsDto);
        await _unitOfWork.Complete();
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific News.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(policy: AppPolicy.RequireAdminRole)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _unitOfWork.NewsRepository.DeleteNewsAsync(id);
        await _unitOfWork.Complete();
        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Interfaces;

namespace NewsAPI.Controllers;

public class UploadController(IUnitOfWork _unitOfWork) : IController
{


    // [HttpPost]
    // public async Task<ActionResult<PhotoDto>> UploadFile(UploadDto uploadDto)
    // {
    //     return await _uploadRepository.UploadAsync(uploadDto);
    // }

    [HttpPost("list")]
    [Authorize(policy: AppPolicy.RequireModeratorRole)]
    public async Task<ActionResult<IEnumerable<PhotoDto>>> UploadList(List<UploadDto> uploadDtos)
    {
        var images = await _unitOfWork.UploadRepository.UploadListAsync(uploadDtos);
        await _unitOfWork.Complete();
        return Ok(images);
    }

    [HttpDelete("{url}")]
    [Authorize(policy: AppPolicy.RequireModeratorRole)]
    public async Task<ActionResult> DeleteAsync(string url)
    {
        await _unitOfWork.UploadRepository.DeleteAsync(url);
        return NoContent();
    }


}

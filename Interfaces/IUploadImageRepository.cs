using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IUploadRepository
    {
        Task<ActionResult<PhotoDto>> UploadAsync(UploadDto uploadImageDto);
        Task<ActionResult<IEnumerable<PhotoDto>>> UploadListAsync(List<UploadDto> uploadImagesDto);
        Task<ActionResult> DeleteAsync(string publicId);

    }
}
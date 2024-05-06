using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IUploadRepository
    {
        Task<PhotoDto> UploadAsync(UploadDto uploadImageDto);
        Task<IEnumerable<PhotoDto>> UploadListAsync(List<UploadDto> uploadImagesDto);

        Task<List<PhotoDto>> GetPhotosByUrls(List<string> urls);

        Task DeleteAsync(string url);

    }
}
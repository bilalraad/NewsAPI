using NewsAPI.DTOs;

namespace NewsAPI.Interfaces
{
    public interface IUploadImageRepository
    {
        Task<PhotoDto> UploadImageAsync(UploadImageDto uploadImageDto);
        Task<List<PhotoDto>> UploadImagesAsync(List<UploadImageDto> uploadImagesDto);
        Task<PhotoDto> DeleteImageAsync(string publicId);

    }
}
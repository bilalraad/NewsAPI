using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsAPI.DTOs;
using NewsAPI.Entities;
using NewsAPI.Errors;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;

namespace NewsAPI.Repositories
{
    public class UploadRepository : IUploadRepository
    {

        private readonly Cloudinary _cloudinary;
        private readonly Context _context;
        private readonly IMapper _mapper;

        private readonly ILogger<UploadRepository> _logger;


        public UploadRepository(IOptions<CloudinarySettings> cloudinarySettings, Context context, IMapper mapper, ILogger<UploadRepository> logger)
        {
            Account account = new Account(
                cloudinarySettings.Value.CloudName,
                cloudinarySettings.Value.ApiKey,
                cloudinarySettings.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task DeleteAsync(string url)
        {
            var photo = _context.Photos.FirstOrDefault((p) => p.Url == url);
            if (photo == null) throw AppException.NotFound("Photo not Found");
            var deleteParams = new DeletionParams(photo.PublicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Result == "ok") return;
            throw AppException.BadRequest("failed to delete photo");
        }

        public async Task<List<PhotoDto>> GetPhotosByUrls(List<string> urls)
        {
            var photos = await _context.Photos
                            .Where(p => urls.Contains(p.Url))
                            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            return photos;
        }

        public async Task<PhotoDto> UploadAsync(UploadDto uploadImageDto)
        {

            var uploadResult = await UploadFile(uploadImageDto.File);

            Photo photo = new Photo
            {
                Url = uploadResult.SecureUrl.AbsoluteUri.ToString(),
                PublicId = uploadResult.PublicId,
                Description = uploadImageDto.Description,
                IsMain = uploadImageDto.IsMain,
            };

            await _context.Photos.AddAsync(photo);


            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<IEnumerable<PhotoDto>> UploadListAsync(List<UploadDto> uploadImagesDto)
        {
            if (uploadImagesDto.Count == 0) throw AppException.BadRequest("No files uploaded");
            List<PhotoDto> photos = [];
            if (uploadImagesDto.Count == 0) throw AppException.BadRequest("No files uploaded");
            if (uploadImagesDto.Count(i => i.IsMain) > 1) throw AppException.BadRequest("Only one main image is allowed");
            foreach (var uploadImageDto in uploadImagesDto)
            {
                var uploadResult = UploadFile(uploadImageDto.File).Result;
                Photo photo = new Photo
                {
                    Url = uploadResult.SecureUrl.AbsoluteUri.ToString(),
                    PublicId = uploadResult.PublicId,
                    Description = uploadImageDto.Description,
                    IsMain = uploadImageDto.IsMain,
                };
                await _context.Photos.AddAsync(photo);

                photos.Add(_mapper.Map<PhotoDto>(photo));
            }
            return photos;

        }

        private async Task<ImageUploadResult> UploadFile(IFormFile File)
        {
            var uploadResult = new ImageUploadResult();

            if (File.Length > 0)
            {
                using (var stream = File.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(File.FileName, stream),
                        Folder = "news",
                        Transformation = new Transformation().Height(1080).Width(1080)
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            return uploadResult;

        }
    }
}
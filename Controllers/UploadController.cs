using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Controllers
{
    [Route("[controller]")]
    public class UploadController : BaseController
    {
        private readonly ILogger<UploadController> _logger;

        private readonly IUploadRepository _uploadRepository;


        public UploadController(ILogger<UploadController> logger, IUploadRepository uploadRepository)
        {
            _logger = logger;
            _uploadRepository = uploadRepository;
        }

        // [HttpPost]
        // public async Task<ActionResult<PhotoDto>> UploadFile(UploadDto uploadDto)
        // {
        //     return await _uploadRepository.UploadAsync(uploadDto);
        // }

        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> UploadList(List<UploadDto> uploadDtos)
        {
            return Ok(await _uploadRepository.UploadListAsync(uploadDtos));
        }

        [HttpDelete("{url}")]
        public async Task<ActionResult> DeleteAsync(string url)
        {
            await _uploadRepository.DeleteAsync(url);
            return NoContent();
        }


    }
}
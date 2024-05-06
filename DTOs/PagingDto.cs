using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs
{
    public class PagingDto
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;
        [Range(1, 1000), DefaultValue(10)]
        public int PageSize { get; set; } = 10;

    }
}
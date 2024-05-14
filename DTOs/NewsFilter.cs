using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NewsAPI.DTOs;

public class NewsFilter : PagingDto
{
    public string? Search { get; set; }
    public List<string>? Tags { get; set; }

    public SortingDto Sorting { get; set; } = new SortingDto();
}



[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderBy
{
    CreatedAt,
    UpdatedAt,
    ViewCount,
}

public class SortingDto
{
    [DefaultValue("UpdatedAt")]
    public OrderBy OrderBy { get; set; } = OrderBy.UpdatedAt;
    [DefaultValue(true)]
    public bool IsDescending { get; set; } = true;

}

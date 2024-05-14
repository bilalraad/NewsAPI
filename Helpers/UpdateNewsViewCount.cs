using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewsAPI.DTOs;
using NewsAPI.Interfaces;

namespace NewsAPI.Helpers
{
    public class UpdateNewsViewCount : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();

            if (result.Result is OkObjectResult)
            {
                var newsId = context.RouteData.Values.Last().Value!.ToString();
                var dbContext = context.HttpContext.RequestServices.GetRequiredService<Context>();
                var news = await dbContext.News.FindAsync(Guid.Parse(newsId!));
                news!.ViewCount++;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
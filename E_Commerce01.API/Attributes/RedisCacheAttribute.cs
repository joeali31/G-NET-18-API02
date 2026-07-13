using E_Commerce01.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce01.API.Attributes
{
    public class RedisCacheAttribute(int timeInSeconds) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GetCacheKey(context.HttpContext.Request);

            var value = await cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(value))
            {
                context.Result = new ContentResult()
                {
                    Content = value,
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                return;
            }
            else
            {
                var executedEndPoint = await next.Invoke();
                if(executedEndPoint.Result is OkObjectResult okResult)
                {
                    await cacheService.SetAsync(cacheKey , okResult.Value , TimeSpan.FromSeconds(timeInSeconds));
                }
            }

        }

        private string GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query)
            {
                key.Append($"{item.Key} | {item.Value}");
            }

            return key.ToString();
        }


    }
}

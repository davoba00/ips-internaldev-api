
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RebuildProject.Models;

namespace RebuildProject.Middleware
{
    public class RequestMiddleware : IMiddleware
    {
        private readonly DbLoggingSettings settings;

        public RequestMiddleware(IOptions<DbLoggingSettings> options)
        {
            this.settings = options.Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!settings.Enable)
            {
                await next(context);
                return;
            }
            var request = context.Request;

            var log = new ApiLog
            {
                LogId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                RequestTime = DateTime.UtcNow,
                RequestHeaders = JsonConvert.SerializeObject(request.Headers),
                QueryString = request.QueryString.ToString(),
                RequestUrl = request.GetDisplayUrl(),
                RequestMethod = request.Method
            };

            context.Items["ApiLog"] = log;

            await next(context);

        }
    }
}

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RebuildProject.Models;

namespace RebuildProject.Middleware
{
    public class RequestMiddleware : IMiddleware
    {
        private readonly DbLoggingSettings settings;

        public RequestMiddleware(IOptionsMonitor<DbLoggingSettings> options)
        {
            this.settings = options.CurrentValue;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!settings.Enable)
            {
                await next(context);
                return;
            }

            var shouldLog = this.ShouldLog(context.Request);

            if (!shouldLog)
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

            context.Features.Set<ApiLog>(log);

            await next(context);

        }

        private bool ShouldLog(HttpRequest request)
        {
            if (!request.Path.HasValue)
            {
                return false;
            }

            var excudedEndpoints = new List<string>
            {
                "/$metadata",
                "/%24metadata",
                "/%2524metadata"
            };

            var isExisiting = excudedEndpoints.Any(x => request.Path.Value.Contains(x, StringComparison.InvariantCultureIgnoreCase));

            if (isExisiting)
            {
                return false;
            }

            return true;
        }
    }
}

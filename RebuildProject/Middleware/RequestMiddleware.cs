using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RebuildProject.Models;

namespace RebuildProject.Middleware
{
    // TODO: remove
    // - remove extra spaces, add one space between codes
    //
    public class RequestMiddleware : IMiddleware
    {
        #region Fields

        private readonly DbLoggingSettings settings;

        #endregion

        #region Constructor

        public RequestMiddleware(IOptionsMonitor<DbLoggingSettings> options)
        {
            this.settings = options.CurrentValue;
        }

        #endregion

        #region Public Methods

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

        #endregion

        #region Private Methods

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

            // TODO: Remove variable, put condition in brackes
            //
            var isExisiting = excudedEndpoints.Any(x => request.Path.Value.Contains(x, StringComparison.InvariantCultureIgnoreCase));

            if (isExisiting)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}

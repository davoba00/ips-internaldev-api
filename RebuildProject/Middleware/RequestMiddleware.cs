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

            // Da definesem flag koji bi bio should log, da li treba da zapocem u zavisnosti od uslova, ako naletim na $metadata, should log treba da vrati false

            //Ne loguj mi req za metadata, odradi



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
    }
}

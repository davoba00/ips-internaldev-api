using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RebuildProject.Extensions;
using RebuildProject.Models;
using RebuildProject.Service;
using System.Net;

namespace RebuildProject.Middleware
{
    public class ApiLoggingMiddleware : IMiddleware
    {
        #region Fields

        private readonly IMediator mediator;
        private readonly IOptionsMonitor<DbLoggingSettings> options;

        #endregion

        #region Constructor

        public ApiLoggingMiddleware(IMediator mediator, IOptionsMonitor<DbLoggingSettings> options)
        {
            this.mediator = mediator;
            this.options = options;
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var log = context.Features.Get<ApiLog>();

            if (log == null)
            {
                await next(context);
                return;
            }

            context.Request.EnableBuffering();

            string responseBody;
            Stream originalBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await next(context);

                responseBody = await memoryStream.ReadToEndAsync();

                await memoryStream.CopyToAsync(originalBody);
            }

            context.Response.Body = originalBody;

            var respone = context.Response;

            log.ResponseStatus = respone.StatusCode.ToString();
            log.ResponseHeaders = JsonConvert.SerializeObject(respone.Headers);
            log.ResponseTime = DateTime.UtcNow;
            log.ContentType = respone.ContentType;

            if (respone.StatusCode >= (int)HttpStatusCode.BadRequest)
            {
                log.ErrorMessage = string.IsNullOrEmpty(responseBody)
                    ? respone.StatusCode.ToString()
                    : responseBody;
            }

            await this.mediator.Send(new AddApiLogCommand
            {
                ApiLog = log,
                DaysToKeepLogEntry = this.options.CurrentValue.DaysToKeepLogEntry
            });
        }

        #endregion
    }
}

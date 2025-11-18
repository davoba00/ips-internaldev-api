using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using RebuildProject.Extensions;
using RebuildProject.Models;
using RebuildProject.Service;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace RebuildProject.Middleware
{
    //public class ApiLoggingMiddleware
    //{
    //    private readonly RequestDelegate next;
    //    private readonly IServiceScopeFactory scopeFactory;

    //    public ApiLoggingMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    //    {
    //        this.next = next;
    //        this.scopeFactory = scopeFactory;
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        ApiLog apiLog = new ApiLog();
    //        apiLog.LogId = Guid.NewGuid();
    //        apiLog.RequestId = Guid.NewGuid();

    //        apiLog.RequestTime = DateTime.UtcNow;
    //        apiLog.RequestUrl = context.Request.Path + context.Request.QueryString;
    //        apiLog.RequestMethod = context.Request.Method;
    //        apiLog.RequestHeaders = context.Request.Headers.ToString();
    //        apiLog.QueryString = context.Request.QueryString.ToString();

    //        try
    //        {
    //            await next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //            apiLog.ResponseStatus = "500";
    //            apiLog.ErrorMessage = ex.Message;
    //            apiLog.StackTrace = ex.StackTrace;
    //            apiLog.ResponseTime = DateTime.UtcNow;

    //            using var scope = scopeFactory.CreateScope();
    //            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    //            await mediator.Send(new AddApiLogCommand { ApiLog = apiLog });

    //            throw;
    //        }

    //        apiLog.ResponseTime = DateTime.UtcNow;
    //        apiLog.ResponseStatus = context.Response.StatusCode.ToString();
    //        apiLog.ResponseHeaders = context.Response.Headers.ToString();
    //        apiLog.ContentType = context.Response.ContentType;

    //        using (var scope = scopeFactory.CreateScope())
    //        {
    //            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    //            await mediator.Send(new AddApiLogCommand { ApiLog = apiLog });
    //        }
    //    }
    //}

    public class ApiLoggingMiddleware : IMiddleware
    {
        private readonly IMediator mediator;

        public ApiLoggingMiddleware(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //request 

            // - get request information 
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

            //response 

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
                ApiLog = log
            });

        }
    }
}

using FluentResults;
using MediatR.Pipeline;
using Microsoft.Data.SqlClient;
using NLog;
using RebuildProject.Common;
using RebuildProject.Extensions;
using RebuildProject.Service;
using System;
using System.Data.Common;
using System.Net;
using static RebuildProject.Common.Enums;

// TODO: remove
// - remove unused `usings`, `commented codes`, `class`
// - add `this.` 
//
namespace RebuildProject.MediatR
{
    public class GlobalRequestExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, Exception>
        where TResponse : Result, new()
    {
        #region Fields

        private readonly Logger logger;

        #endregion

        #region Constructors

        public GlobalRequestExceptionHandler()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        #endregion

        #region Public Methods

        public Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            this.logger.Error(exception.Flatten(includeStackTrace: true));

            var errorObject = new Error(exception.Message);

            foreach (var ex in exception.GetAllExceptions())
            {
                switch (ex)
                {
                    case OperationCanceledException operationCanceledException:
                        errorObject.WithMetadata(nameof(ErrorMetadataKey.Status), StatusCodes.Status499ClientClosedRequest);

                        errorObject.WithMetadata(nameof(OperationCanceledException), operationCanceledException.Message);

                        break;

                    case DbException dbException:
                        // MS SQL
                        //
                        if (dbException is Microsoft.Data.SqlClient.SqlException sqlEx)
                        {
                            var (status, message) = SqlErrorCatalog.Resolve(sqlEx);

                            errorObject.WithMetadata(nameof(ErrorMetadataKey.Status), status);
                            errorObject.WithMetadata(nameof(SqlException), message);
                        }

                        break;
                }
            }

            if (!errorObject.HasMetadataKey(nameof(ErrorMetadataKey.Status)))
            {
                var mainEx = exception.GetLowestInnerException();

                errorObject.WithMetadata(nameof(ErrorMetadataKey.Status), StatusCodes.Status500InternalServerError);

                errorObject.WithMetadata(mainEx.GetType().Name, mainEx.Message);
            }

            var resultFailed = Result.Fail(errorObject);

            var response = new TResponse();
            response.WithErrors(resultFailed.Errors);

            state.SetHandled(response);

            return Task.CompletedTask;
        }

        #endregion
    }

    public static class SqlErrorHttpMapper
    {
        #region Fields
        public static readonly Dictionary<int, int> Map = new()
    {
        // 🔹 User input / syntax errors → 400 Bad Request
        { 102,  StatusCodes.Status400BadRequest },
        { 207,  StatusCodes.Status400BadRequest },
        { 208,  StatusCodes.Status400BadRequest },
        { 245,  StatusCodes.Status400BadRequest },
        { 8114, StatusCodes.Status400BadRequest },
        { 8115, StatusCodes.Status400BadRequest },
        { 2628, StatusCodes.Status400BadRequest },
        { 8152, StatusCodes.Status400BadRequest },
        { 402,  StatusCodes.Status400BadRequest },
        { 213,  StatusCodes.Status400BadRequest },
        { 2456, StatusCodes.Status400BadRequest }, 
        
        // 🔹 Duplicate / constraint violations → 409 Conflict
        { 2627, StatusCodes.Status409Conflict },
        { 2601, StatusCodes.Status409Conflict },
        { 547,  StatusCodes.Status409Conflict },
        { 2714, StatusCodes.Status409Conflict },
        
        // 🔹 Permission / security → 403 Forbidden / 401 Unauthorized
        { 229,  StatusCodes.Status403Forbidden },
        { 230,  StatusCodes.Status403Forbidden },
        { 15151,StatusCodes.Status403Forbidden },
        { 18456,StatusCodes.Status401Unauthorized },
        
        // 🔹 Resource not found → 404 Not Found
        { 2812, StatusCodes.Status404NotFound },
        { 8144, StatusCodes.Status404NotFound },
        { 2819, StatusCodes.Status404NotFound },
        
        // 🔹 Deadlocks, connection, timeout → 503/504
        { -2,   StatusCodes.Status504GatewayTimeout },
        { 1205, StatusCodes.Status503ServiceUnavailable },
        { 4060, StatusCodes.Status503ServiceUnavailable },
        { 64,   StatusCodes.Status503ServiceUnavailable },
        { 233,  StatusCodes.Status503ServiceUnavailable },
        { 10054,StatusCodes.Status503ServiceUnavailable },
        { 10060,StatusCodes.Status503ServiceUnavailable },
        
        // 🔹 Server internal / unhandled → 500 Internal Server Error
        { 50000, StatusCodes.Status500InternalServerError },
        { 18054, StatusCodes.Status500InternalServerError },
        { 6001,  StatusCodes.Status500InternalServerError },
        { 0,     StatusCodes.Status500InternalServerError }
    };

        #endregion

        #region Public Methods
        public static int GetHttpStatus(int sqlErrorNumber)
            => Map.TryGetValue(sqlErrorNumber, out var status)
                ? status
                : StatusCodes.Status500InternalServerError;
        
        #endregion
    }
}

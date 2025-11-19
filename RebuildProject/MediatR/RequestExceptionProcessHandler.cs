using FluentResults;
using MediatR.Pipeline;
using Microsoft.Data.SqlClient;
using NLog;
using RebuildProject.Common;
using RebuildProject.Extensions;
using System.Data.Common;
using static RebuildProject.Common.Enums;


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
}

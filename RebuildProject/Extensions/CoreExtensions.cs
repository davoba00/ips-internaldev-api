using FluentResults;
using System.Net;
using System.Text;
using static RebuildProject.Common.Enums;

namespace RebuildProject.Extensions
{
    public static class CoreExtensions
    {
        public static string Flatten(this Exception exception, string message = "", bool includeStackTrace = false)
        {
            StringBuilder stringBuilder = new StringBuilder(message);

            Exception currentException = exception;

            while (currentException != null)
            {
                stringBuilder.AppendLine(currentException.Message);

                if (includeStackTrace)
                {
                    stringBuilder.Append(exception.StackTrace);
                }

                currentException = currentException.InnerException;

                if (!includeStackTrace)
                {
                    continue;
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static IEnumerable<Exception> GetAllExceptions(this Exception exception)
        {
            yield return exception;

            if (exception is AggregateException aggrEx)
            {
                foreach (Exception innerEx in aggrEx.InnerExceptions.SelectMany(e => e.GetAllExceptions()))
                {
                    yield return innerEx;
                }
            }
            else if (exception.InnerException != null)
            {
                foreach (Exception innerEx in exception.InnerException.GetAllExceptions())
                {
                    yield return innerEx;
                }
            }
        }

        public static Exception GetLowestInnerException(this Exception exception)
        {
            if (exception?.InnerException == null)
            {
                return exception;
            }

            Exception innerException = exception.InnerException;

            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }

            return innerException;
        }

        public static int GetErrorStatusCode(this Result result)
        {
            var statusCode = result.Errors.FirstOrDefault()?.Metadata?.GetValueOrDefault(nameof(ErrorMetadataKey.Status)) ?? (int)HttpStatusCode.InternalServerError;

            return (int)statusCode;
        }
    }


}

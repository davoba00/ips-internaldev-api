using Microsoft.Data.SqlClient;
using System.Net;

namespace RebuildProject.Common
{
    public static class SqlErrorCatalog
    {
        // Data Integration
        //
        public static class Constraint
        {
            public const int DuplicateKey = 2627;
            public const int UniqueIndex = 2601;
            public const int ForeignKey = 547;
            public const int NullViolation = 515;
        }

        // Transaction \ Concurrency
        public static class Transaction
        {
            public const int Deadlock = 1205;
        }

        // syntax \ Invalid Query
        public static class Syntax
        {
            public const int InvalidColumn = 207;
            public const int InvalidObject = 208;
            public const int ConversionFailed = 245;
            public const int InvalidDataType = 8114;
            public const int DivideByZero = 8134;
            public const int DataTruncation = 8152;
            public const int DataTruncationExtented = 2628;

        }

        // Connection \ Timeout
        public static class Connection
        {
            public const int ConnectionFailed = 53;
            public const int TimeoutExpired = -2;
        }

        // General \ Unknown
        public static class General
        {
            public const int InternalError = 1000;
        }

        internal static readonly IReadOnlyDictionary<int, HttpStatusCode> Map = new Dictionary<int, HttpStatusCode>
        {
            // constraint
            [Constraint.DuplicateKey] = HttpStatusCode.Conflict,
            [Constraint.UniqueIndex] = HttpStatusCode.Conflict,
            [Constraint.ForeignKey] = HttpStatusCode.BadRequest,
            [Constraint.NullViolation] = HttpStatusCode.BadRequest,

            // transaction
            [Transaction.Deadlock] = HttpStatusCode.Conflict,

            // syntax
            [Syntax.InvalidColumn] = HttpStatusCode.BadRequest,
            [Syntax.InvalidObject] = HttpStatusCode.BadRequest,
            [Syntax.ConversionFailed] = HttpStatusCode.BadRequest,
            [Syntax.DataTruncationExtented] = HttpStatusCode.BadRequest,

            // connection
            [Connection.ConnectionFailed] = HttpStatusCode.ServiceUnavailable,
            [Connection.TimeoutExpired] = HttpStatusCode.RequestTimeout,

            // general
            [General.InternalError] = HttpStatusCode.InternalServerError
        };

        // Helper Methods
        //
        public static (HttpStatusCode Status, string Message) Resolve(SqlException ex)
        {
            var message = $"Number: {ex.Number} - {ex.Message}";

            if (Map.TryGetValue(ex.Number, out var code))
            {
                return (code, message);
            }

            return (HttpStatusCode.InternalServerError, message);
        }
    }
}

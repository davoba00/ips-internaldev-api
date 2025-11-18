namespace RebuildProject.Middleware
{
    public static class ApiLoggingMiddlewareExtensions
    {
        #region Public Methods

        public static IApplicationBuilder UseApiLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiLoggingMiddleware>();
        }

        #endregion
    }
}

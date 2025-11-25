namespace RebuildProject.Middleware
{
    public static class MiddlewareExtensions
    {
        #region Public Methods

        public static IApplicationBuilder UseApiLogging(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ApiLoggingMiddleware>();
        }

        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<RequestMiddleware>();
        }

        #endregion
    }
}

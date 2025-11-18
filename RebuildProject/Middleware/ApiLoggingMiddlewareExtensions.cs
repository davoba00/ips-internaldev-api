namespace RebuildProject.Middleware
{
    public static class ApiLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiLoggingMiddleware>();
        }
    }
}

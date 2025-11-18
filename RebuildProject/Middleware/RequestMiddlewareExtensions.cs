namespace RebuildProject.Middleware
{
    public static class RequestMiddlewareExtensions
    {
        #region Public Methods

        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }

        #endregion
    }
}


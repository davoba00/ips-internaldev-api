using MediatR;

namespace RebuildProject.MediatR
{
    public class RequestCancellationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor accessor;

        public RequestCancellationBehavior(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var token = accessor.HttpContext?.RequestAborted ?? cancellationToken;

            return await next(token);
        }
    }
}

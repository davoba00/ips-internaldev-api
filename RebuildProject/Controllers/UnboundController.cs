using MediatR;
using Microsoft.AspNetCore.Mvc;
using RebuildProject.Models;
using RebuildProject.Service;
using static RebuildProject.Common.Constants;

namespace RebuildProject.Controllers
{
    [Route(ApiRoutes.Default)]
    [ApiController]
    public class UnboundController : BaseODataController
    {
        #region Constructor

        public UnboundController(IMediator mediator) : base(mediator)
        {
        }

        #endregion

        #region Unbound Function

        [HttpGet("GetAllResource")]
        public async Task<IActionResult> GetAllResource()
        {
            var result = await mediator.Send(new GetResourcesQuery { });

            if (result.IsFailed || result.Resources == null)
            {
                return this.StatusCode(result);
            }

            return Ok(result.Resources);
        }

        [HttpGet("mostRecent")]
        public async Task<IActionResult> MostRecent()
        {
            var result = await mediator.Send(new GetMaxResourceQuery { });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.Resource);
        }

        #endregion

        #region Unbound Action

        [HttpPost("incrementResource")]
        public async Task<IActionResult> IncrementResource([FromBody] Resource resource)
        {
            var result = await this.mediator.Send(new AddResourceCommand { Resource = resource });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.Resource);
        }

        #endregion
    }
}

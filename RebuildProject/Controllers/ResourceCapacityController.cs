using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;
using RebuildProject.Service;
using static RebuildProject.Common.Constants;

namespace RebuildProject.Controllers
{
    [Route(ApiRoutes.Default)]
    [ApiController]
    public class ResourceCapacityController : BaseODataController
    {
        public ResourceCapacityController(IMediator mediator) : base(mediator)
        {
        }

        #region Public Methods

        [EnableQuery]
        [HttpGet("resourceCapacity")]
        public async Task<ActionResult> Get(ODataQueryOptions<ResourceCapacity> queryOptions)
        {
            var result = await mediator.Send(new GetResourcesCapacityQuery
            {
                QueryOptions = queryOptions
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ResourcesCapacity);
        }

        [EnableQuery]
        [HttpGet("resourceCapacity/{id:guid}")]
        public async Task<IActionResult> Get(Guid id, ODataQueryOptions<ResourceCapacity> queryOptions)
        {
            var result = await mediator.Send(new GetResourceCapacityQuery
            {
                QueryOptions = queryOptions,
                Id = id
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ResourceCapacity);
        }

        [HttpPost("resourceCapacity")]
        public async Task<IActionResult> Post([FromBody] ResourceCapacity resourceCapacity)
        {
            var result = await this.mediator.Send(new AddResourceCapacityCommand
            {
                ResourceCapacity = resourceCapacity
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ResourceCapacity);

        }

        [HttpDelete("resourceCapacity/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await this.mediator.Send(new DeleteResourceCapacityCommand
            {
                Id = id
            });

            var result = await mediator.Send(new GetResourceCapacityQuery
            {
                Id = id
            });

            if (deleteResult.IsFailed)
            {
                return this.StatusCode(deleteResult);
            }

            return Ok();

        }

        [HttpPatch("resourceCapacity/{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Delta<ResourceCapacity> delta)
        {
            var resoure = await this.mediator.Send(new PatchResourceCapacityCommand
            {
                Id = id,
                Delta = delta
            });

            if (resoure.IsFailed)
            {
                return this.StatusCode(resoure);
            }

            return Ok(resoure.ResourceCapacity);
        }

        #endregion

    }
}

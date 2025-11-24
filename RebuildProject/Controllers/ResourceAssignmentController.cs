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
    public class ResourceAssignmentController : BaseODataController
    {
        #region Constructor

        public ResourceAssignmentController(IMediator mediator) : base(mediator)
        {
        }

        #endregion

        #region Public Methods

        [EnableQuery]
        [HttpGet("resourceAssignment")]
        public async Task<ActionResult> Get(ODataQueryOptions<ResourceAssignment> queryOptions)
        {
            var result = await mediator.Send(new GetResourcesAssignmentQuery
            {
                QueryOptions = queryOptions
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ResourceAssignment);
        }

        [EnableQuery]
        [HttpGet("resourceAssignment/{id:guid}")]
        public async Task<IActionResult> Get(Guid id, ODataQueryOptions<ResourceAssignment> queryOptions)
        {
            var result = await mediator.Send(new GetResourceAssignmentQuery
            {
               QueryOptions = queryOptions,
               Id = id
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ResourceAssignment);
        }

        [HttpPost("resourceAssignment")]
        public async Task<IActionResult> Post([FromBody] ResourceAssignment resourceAssignment)
        {
            var result = await this.mediator.Send(new AddResourceAssignmentCommand
            {
                ResourceAssignment = resourceAssignment
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ResourceAssignment);

        }

        [HttpPatch("resourceAssignment/{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Delta<ResourceAssignment> delta)
        {
            var resoure = await this.mediator.Send(new PatchResourceAssignmentCommand
            {
                Id = id,
                Delta = delta
            });

            if (resoure.IsFailed)
            {
                return this.StatusCode(resoure);
            }

            return Ok(resoure.ResourceAssignment);
        }

        [HttpDelete("resourceAssignment/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.mediator.Send(new DeleteResourceAssignmentCommand
            {
                Id = id
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return this.NoContent();

        }

        #endregion
    }
}

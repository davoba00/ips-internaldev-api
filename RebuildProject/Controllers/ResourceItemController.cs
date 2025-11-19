using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using RebuildProject.Models;
using RebuildProject.Service;
using static RebuildProject.Common.Constants;

namespace RebuildProject.Controllers
{
    [Route(ApiRoutes.Default)]
    [ApiController]
    public class ResourceItemController : BaseODataController
    {
        #region Constructors

        public ResourceItemController(IMediator mediator) : base(mediator) 
        {
            
        }

        #endregion

        #region Public Methods

        [EnableQuery]
        [HttpGet("resourceitem")]
        public async Task<IQueryable<ResourceItem>> Get(ODataQueryOptions<ResourceItem> queryOptions)
        {
            var result = await this.mediator.Send(new GetResourcesItemQuery
            {
                QueryOptions = queryOptions
            });

            return result.Resources;
        }

        [EnableQuery]
        [HttpGet("resourceitem/{id}")]
        public async Task<SingleResult<ResourceItem>> Get(Guid id, ODataQueryOptions<ResourceItem> queryOptions)
        {
            var result = await mediator.Send(new GetResourceItemQuery
            {
                QueryOptions = queryOptions,
                Id = id
            });

            return SingleResult.Create(result.Resource);
        }

        [HttpPost("resourceItem")]
        public async Task<IActionResult> Post([FromBody] ResourceItem resource)
        {
            var added = await mediator.Send(new AddResourceItemCommand
            {
                ResourceItem = resource
            });

            if (added.IsFailed)
            {
                return this.StatusCode(added);
            }

            return Ok(added.Resource);
        }

        [HttpDelete("resourceItem/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteResourceItemCommand
            {
                Id = id
            });

            var result = await mediator.Send(new GetResourceItemQuery
            {
                Id = id
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return NoContent();
        }

        [HttpPatch("resourceItem/{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Delta<ResourceItem> delta)
        {
            var resoure = await mediator.Send(new PatchResourceItemCommand
            {
                Id = id,
                Delta = delta
            });

            if (resoure.IsFailed)
            {
                return this.StatusCode(resoure);
            }

            return Ok(resoure.Resources);
        }

        #endregion
    }
}

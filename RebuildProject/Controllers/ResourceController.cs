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
    public class ResourceController : BaseODataController
    {
        #region Constructors

        public ResourceController(IMediator mediator) : base(mediator)
        {

        }

        #endregion

        #region Public Methods

        [EnableQuery]
        [HttpGet("resource")]
        public async Task<IActionResult> Get(ODataQueryOptions<Resource> queryOptions)
        {
            var result = await mediator.Send(new GetResourcesQuery
            {
                QueryOptions = queryOptions
            });

            if (result.IsFailed || result.Resources == null)
            {
                return this.StatusCode(result);
            }

            return Ok(result.Resources);
        }

        [EnableQuery]
        [HttpGet("resource/{id}")]
        public async Task<SingleResult<Resource>> Get(Guid id, ODataQueryOptions<Resource> queryOptions)
        {
            var result = await this.mediator.Send(new GetResourceQuery
            {
                QueryOptions = queryOptions,
                Id = id
            });

            if (result.IsFailed || result.Resource == null)
            {
                var empty = Enumerable.Empty<Resource>().AsQueryable();

                return SingleResult.Create(empty);
            }

            return SingleResult.Create(result.Resource);
        }

        [HttpPost("resource")]
        public async Task<IActionResult> Post([FromBody] Resource resource)
        {
            var result = await this.mediator.Send(new AddResourceCommand
            {
                Resource = resource
            });

            if (result.IsFailed)
            {
                #region OldCode

                //var vv = result.Errors.FirstOrDefault()?.Metadata?.GetValueOrDefault("Status");

                //var statusCode = vv != null ? (int)vv : (int)HttpStatusCode.InternalServerError;

                //return this.StatusCode(statusCode, result.Errors);

                #endregion

                return this.StatusCode(result);
            }

            return Ok(result.Resource);
        }

        [HttpDelete("resource/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await this.mediator.Send(new DeleteResourceCommand
            {
                Id = id
            });

            var result = await mediator.Send(new GetResourceQuery
            {
                Id = id
            });

            if (deleteResult.IsFailed)
            {
                return this.StatusCode(deleteResult);
            }

            return Ok(result);
        }

        [HttpPatch("resource/{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Delta<Resource> delta)
        {
            var resoure = await this.mediator.Send(new PatchResourceCommand
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

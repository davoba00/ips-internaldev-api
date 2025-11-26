

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using RebuildProject.Models;
using RebuildProject.Service;
using static RebuildProject.Common.Constants;

namespace RebuildProject.Controllers
{

    [Route(ApiRoutes.ApiMob)]
    [ApiController]
    public class TestController : BaseODataController
    {
        public TestController(IMediator mediator) : base(mediator)
        {
        }

        [EnableQuery]
        [HttpGet("GetResourcesData")]
        public async Task<IActionResult> GetResourcesData(ODataQueryOptions<Resource> queryOptions)
        {
            var result = await mediator.Send(new GetResourcesQuery
            {
                QueryOptions = queryOptions
            });

            return this.Ok(result.Resources);
        }

        [EnableQuery]
        [HttpGet("GetResourceData")]
        public async Task<SingleResult<Resource>> GetResourceData(ODataQueryOptions<Resource> queryOptions, Guid Id)
        {
            var result = await mediator.Send(new GetResourceQuery
            {
                Id = Id,
                QueryOptions = queryOptions
            });

            return SingleResult.Create(result.Resource);
        }

        [EnableQuery]
        [HttpGet("GetResourceView")]
        public async Task<IActionResult> GetResourceView(ODataQueryOptions<ResourceViewDto> queryOptions, Guid Id)
        {
            var result = await mediator.Send(new GetResourceQuery
            {
                Id = Id
            });

            var result2 = new ResourceViewDto
            {
                Resource = result.Resource.FirstOrDefault(),
                ResourceAssignmentCount = 5,
                ResourceCapacityCount = 2,
                ResourceItemCount = 11
            };

            return this.Ok(result2);
        }
    }
}

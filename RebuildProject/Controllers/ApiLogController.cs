using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using RebuildProject.Models;
using RebuildProject.Service;
using static RebuildProject.Common.Constants;

namespace RebuildProject.Controllers
{
    [Route(ApiRoutes.Default)]
    [ApiController]
    public class ApiLogController : BaseODataController
    {
        #region Constructor

        public ApiLogController(IMediator mediator) : base(mediator)
        {
        }

        #endregion

        #region Public Methods

        [EnableQuery]
        [HttpGet("apilog")]
        public async Task<IQueryable<ApiLog>> Get(ODataQueryOptions<ApiLog> options)
        {
            var result = await mediator.Send(new GetApiLogsQuery
            {
                QueryOptions = options
            });

            return result.ApiLogs;
        }

        [EnableQuery]
        [HttpGet("apilog/{id}")]
        public async Task<SingleResult<ApiLog>> Get(Guid id, ODataQueryOptions<ApiLog> options)
        {
            var result = await mediator.Send(new GetApiLogQuery
            {
                Id = id,
                QueryOptions = options
            });

            return SingleResult.Create(result.ApiLog);
        }


        [EnableQuery]
        [HttpPost("apilog")]
        public async Task<IActionResult> Post([FromBody] ApiLog apilog)
        {
            var result = await mediator.Send(new AddApiLogCommand
            {
                ApiLog = apilog
            });

            if (result.IsFailed)
            {
                return this.StatusCode(result);
            }

            return Ok(result.ApiLog);
        }

        #endregion
    }
}

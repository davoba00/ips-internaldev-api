using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{

    public partial class GetApiLogsQuery
    {
        public ODataQueryOptions<ApiLog> QueryOptions { get; set; }
    }

    public partial class GetApiLogsResult
    {
        public IQueryable<ApiLog> ApiLogs { get; set; }
    }

    public class GetApiLogsService : IGetApiLogsService
    {
        private readonly AppDbContext db;
        public GetApiLogsService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<GetApiLogsResult> ApiLogs(GetApiLogsQuery query)
        {
            var list = db.ApiLogs.AsQueryable();
            return await Task.FromResult(new GetApiLogsResult
            {
                ApiLogs = list
            });
        }

    }
}


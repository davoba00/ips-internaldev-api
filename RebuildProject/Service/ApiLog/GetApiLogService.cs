using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    public partial class GetApiLogQuery
    {
        public ODataQueryOptions<ApiLog> QueryOptions { get; set; }
        public Guid Id { get; set; }
    }

    public partial class GetApiLogResult
    {
        public IQueryable<ApiLog> ApiLog { get; set; }
    }

    public class GetApiLogService : IGetApiLogService
    {
        private readonly AppDbContext db;
        public GetApiLogService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<GetApiLogResult> ApiLog(GetApiLogQuery query)
        {
            //var dataSql = db.ApiLogs.Where(x => x.LogId == query.Id);

            return await Task.FromResult(new GetApiLogResult
            {
                //ApiLog = dataSql
            });

        }

    }
}

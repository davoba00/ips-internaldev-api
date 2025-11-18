using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetApiLogsQuery
    {
        public ODataQueryOptions<ApiLog> QueryOptions { get; set; }
    }

    #endregion

    #region Result

    public partial class GetApiLogsResult
    {
        public IQueryable<ApiLog> ApiLogs { get; set; }
    }

    #endregion

    public class GetApiLogsService : IGetApiLogsService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetApiLogsService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetApiLogsResult> ApiLogs(GetApiLogsQuery query)
        {
            var list = db.ApiLogs.AsQueryable();
            return await Task.FromResult(new GetApiLogsResult
            {
                ApiLogs = list
            });
        }

        #endregion

    }
}


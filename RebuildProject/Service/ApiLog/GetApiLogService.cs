using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

// TODO
// - finish the implementation
namespace RebuildProject.Service
{
    #region Query

    public partial class GetApiLogQuery
    {
        public ODataQueryOptions<ApiLog> QueryOptions { get; set; }
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class GetApiLogResult
    {
        public IQueryable<ApiLog> ApiLog { get; set; }
    }

    #endregion

    public class GetApiLogService : IGetApiLogService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetApiLogService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetApiLogResult> ApiLog(GetApiLogQuery query)
        {
            //var dataSql = db.ApiLogs.Where(x => x.LogId == query.Id);

            return await Task.FromResult(new GetApiLogResult
            {
                //ApiLog = dataSql
            });

        }

        #endregion

    }
}

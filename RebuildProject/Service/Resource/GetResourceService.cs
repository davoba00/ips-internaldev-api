using Microsoft.AspNetCore.OData.Query;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourceQuery
    {
        public ODataQueryOptions<Resource> QueryOptions { get; set; }
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourceResult
    {
        public IQueryable<Resource> Resource { get; set; }

    }

    #endregion

    public class GetResourceService : IGetResourceService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourceService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourceResult> GetResource(GetResourceQuery query, CancellationToken cancellationToken)
        {
            var dataSql = db.Resources.Where(x => x.ResourceId == query.Id && x.Deleted == null);

            return await Task.FromResult(new GetResourceResult
            {
                Resource = dataSql
            });
        }

        #endregion
    }
}

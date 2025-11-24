using Microsoft.AspNetCore.OData.Query;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourceItemQuery
    {
        public ODataQueryOptions<ResourceItem> QueryOptions { get; set; }
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourceItemResult
    {
        public IQueryable<ResourceItem> Resource { get; set; }
    }

    #endregion

    public class GetResourceItemService : IGetResourceItemService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourceItemService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourceItemResult> GetResource(GetResourceItemQuery query, CancellationToken cancellationToken)
        {
            var dataSql = db.ResourceItems.Where(x => x.ResourceItemId == query.Id && x.Deleted == null);

            return await Task.FromResult(new GetResourceItemResult
            {
                Resource = dataSql
            });
        }

        #endregion

    }
}

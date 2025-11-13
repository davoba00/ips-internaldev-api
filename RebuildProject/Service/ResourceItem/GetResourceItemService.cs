using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;
using System.Collections.Generic;

namespace RebuildProject.Service
{
    #region Query
    public partial class GetResourceItemQuery
    {
        #region Fields
        public ODataQueryOptions<ResourceItem> QueryOptions { get; set; }
        public Guid Id { get; set; }
        #endregion
    }
    #endregion

    #region Result
    public partial class GetResourceItemResult
    {
        #region Fields
        public IQueryable<ResourceItem> Resource { get; set; }
        #endregion

    }

    #endregion
    public class GetResourceItemService : IGetResourceItemService
    {
        private readonly AppDbContext db;
        public GetResourceItemService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<GetResourceItemResult> GetResource(GetResourceItemQuery query)
        {
            var dataSql = db.ResourceItems.Where(x => x.ResourceItemId == query.Id && x.Deleted == null);

            return await Task.FromResult(new GetResourceItemResult
            {
                Resource = dataSql
            });
        }

    }
}

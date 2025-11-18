using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourcesItemQuery
    {
        public ODataQueryOptions<ResourceItem> QueryOptions { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourcesItemResult
    {
        public IQueryable<ResourceItem> Resources { get; set; }
    }

    #endregion


    public class GetResourcesItemsService : IGetResourcesItemsService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourcesItemsService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourcesItemResult> GetResources(GetResourcesItemQuery query)
        {
            var list = db.ResourceItems.AsQueryable();
            return await Task.FromResult(new GetResourcesItemResult
            {
                Resources = list
            });
        }

        #endregion

    }
}


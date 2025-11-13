using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    public partial class GetResourcesItemQuery
    {
        public ODataQueryOptions<ResourceItem> QueryOptions { get; set; }
    }

    public partial class GetResourcesItemResult
    {
        public IQueryable<ResourceItem> Resources { get; set; }
    }


    public class GetResourcesItemsService : IGetResourcesItemsService
    {
        private readonly AppDbContext db;
        public GetResourcesItemsService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<GetResourcesItemResult> GetResources(GetResourcesItemQuery query)
        {
            var list = db.ResourceItems.AsQueryable();
            return await Task.FromResult(new GetResourcesItemResult
            {
                Resources = list
            });
        }

    }
}


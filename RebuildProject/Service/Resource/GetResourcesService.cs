
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    public partial class GetResourcesQuery
    {
        public ODataQueryOptions<Resource> QueryOptions { get; set; }
    }

    public partial class GetResourcesResult
    {
        public IQueryable<Resource> Resources { get; set; }
    }

    public class GetResourcesService : IGetResourcesService
    {
        private readonly AppDbContext db;
        public GetResourcesService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<GetResourcesResult> GetResources(GetResourcesQuery query)
        {
            var list = db.Resources.AsQueryable();

            return await Task.FromResult(new GetResourcesResult
            {
                Resources = list
            });
        }
    }
}

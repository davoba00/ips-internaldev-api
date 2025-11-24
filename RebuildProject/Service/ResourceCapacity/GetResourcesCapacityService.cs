using Microsoft.AspNetCore.OData.Query;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourcesCapacityQuery
    {
        public ODataQueryOptions<ResourceCapacity> QueryOptions { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourcesCapacityResult
    {
        public IQueryable<ResourceCapacity> ResourcesCapacity { get; set; }
    }

    #endregion

    public class GetResourcesCapacityService : IGetResourcesCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourcesCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourcesCapacityResult> GetResourcesCapacity(GetResourcesCapacityQuery query, CancellationToken token)
        {
            var list = db.ResourceCapacities.AsQueryable();

            return await Task.FromResult(new GetResourcesCapacityResult
            {
                ResourcesCapacity = list
            });
        }

        #endregion
    }
}

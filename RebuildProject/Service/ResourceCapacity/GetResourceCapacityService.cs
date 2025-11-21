
using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourceCapacityQuery
    {
        public ODataQueryOptions<ResourceCapacity> QueryOptions { get; set; }
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourceCapacityResult
    {
        public IQueryable<ResourceCapacity> ResourceCapacity { get; set; }

    }

    #endregion

    public class GetResourceCapacityService : IGetResourceCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourceCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourceCapacityResult> GetResource(GetResourceCapacityQuery query, CancellationToken cancellationToken)
        {
            var dataSql = db.ResourceCapacities.Where(x => x.ResourceId == query.Id && x.Deleted == null);

            return await Task.FromResult(new GetResourceCapacityResult
            {
                ResourceCapacity = dataSql
            });
        }

        #endregion
    }
}

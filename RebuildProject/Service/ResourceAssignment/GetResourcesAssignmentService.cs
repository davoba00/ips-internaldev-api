
using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourcesAssignmentQuery
    {
        public ODataQueryOptions<ResourceAssignment> QueryOptions { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourcesAssignmentResult
    {
        public IQueryable<ResourceAssignment> ResourceAssignment { get; set; }
    }

    #endregion
    public class GetResourcesAssignmentService : IGetResourcesAssignmentService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourcesAssignmentService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods
        public async Task<GetResourcesAssignmentResult> GetResourceAssignment(GetResourcesAssignmentQuery query, CancellationToken cancellationToken)
        {
            var list = db.ResourceAssignments.AsQueryable();

            return await Task.FromResult(new GetResourcesAssignmentResult
            {
                ResourceAssignment = list
            });
        }

        #endregion
    }
}

using Microsoft.AspNetCore.OData.Query;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourceAssignmentQuery
    {
        public ODataQueryOptions<ResourceAssignment> QueryOptions { get; set; }
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourceAssignmenResult
    {
        public IQueryable<ResourceAssignment> ResourceAssignment { get; set; }
    }

    #endregion

    public class GetResourceAssignmentService : IGetResourceAssignmentService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourceAssignmentService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourceAssignmenResult> GetResourceAssignment(GetResourceAssignmentQuery query, CancellationToken cancellationToken)
        {
            var dataSql = db.ResourceAssignments.Where(x => x.ResourceAssignmentId == query.Id && x.Deleted == null);

            return await Task.FromResult(new GetResourceAssignmenResult
            {
                ResourceAssignment = dataSql
            });
        }

        #endregion
    }
}

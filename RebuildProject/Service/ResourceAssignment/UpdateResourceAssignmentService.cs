
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class PatchResourceAssignmentCommand
    {
        public Delta<ResourceAssignment> Delta { get; set; } = default!;
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class PatchResourceAssignmentResult
    {
        public ResourceAssignment ResourceAssignment { get; set; }
    }

    #endregion
    public class UpdateResourceAssignmentService : IUpdateResourceAssingmentService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public UpdateResourceAssignmentService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<PatchResourceAssignmentResult> PatchResource(PatchResourceAssignmentCommand query, CancellationToken cancellationToken)
        {
            var resource = await db.ResourceAssignments.FirstOrDefaultAsync(r => r.ResourceAssignmentId == query.Id);

            if (resource == null)
            {
                var result = new PatchResourceAssignmentResult();

                result.WithError("Resource not found");

                return result;

            }

            query.Delta.Patch(resource);

            resource.UpdateIpsFields(Enums.OperationType.Update);

            await db.SaveChangesAsync(cancellationToken);

            return new PatchResourceAssignmentResult
            {
                ResourceAssignment = resource
            };
        }

        #endregion
    }
}

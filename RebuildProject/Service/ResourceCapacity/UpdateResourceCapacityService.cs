
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class PatchResourceCapacityCommand
    {
        public Delta<ResourceCapacity> Delta { get; set; } = default!;
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class PatchResourceCapacityResult
    {
        public ResourceCapacity ResourceCapacity { get; set; }
    }

    #endregion

    public class UpdateResourceCapacityService : IUpdateResourceCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public UpdateResourceCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<PatchResourceCapacityResult> PatchResource(PatchResourceCapacityCommand query, CancellationToken cancellationToken)
        {
            var resource = await db.ResourceCapacities.FirstOrDefaultAsync(r => r.ResourceCapacityId == query.Id);

            if (resource == null)
            {
                var result = new PatchResourceCapacityResult();

                result.WithError("Resource not found");

                return result;

            }

            query.Delta.Patch(resource);

            resource.UpdateIpsFields(Enums.OperationType.Update);

            await db.SaveChangesAsync(cancellationToken);

            return new PatchResourceCapacityResult
            {
                ResourceCapacity = resource
            };
        }

        #endregion
    }
}

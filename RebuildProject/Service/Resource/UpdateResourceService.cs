using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.EF;
using RebuildProject.Models;
using System.Resources;

namespace RebuildProject.Service
{
    #region Query

    public partial class PatchResourceCommand
    {
        public Delta<Resource> Delta { get; set; } = default!;
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class PatchResourceResult
    {
        public Resource Resources { get; set; }
    }

    #endregion

    public class UpdateResourceService : IUpdateResourceService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public UpdateResourceService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<PatchResourceResult> PatchResource(PatchResourceCommand query, CancellationToken cancellationToken)
        {
            var resource = await db.Resources.FirstOrDefaultAsync(r => r.ResourceId == query.Id);

            if (resource == null)
            {
                var result = new PatchResourceResult();

                result.WithError("Resource not found");

                return result;

            }

            query.Delta.Patch(resource);

            resource.UpdateIpsFields(Enums.OperationType.Update);

            await db.SaveChangesAsync(cancellationToken);

            return new PatchResourceResult
            {
                Resources = resource
            };
        }

        #endregion
    }
}

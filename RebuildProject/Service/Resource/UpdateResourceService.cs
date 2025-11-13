using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query
    public partial class PatchResourceCommand
    {
        #region Fields
        public Delta<Resource> Delta { get; set; } = default!;
        public Guid Id { get; set; }
        #endregion
    }
    #endregion
    public partial class PatchResourceResult
    {
        public Resource Resources { get; set; }
    }

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


        public async Task<PatchResourceResult> PatchResource(PatchResourceCommand query)
        {
            var resource = await db.Resources.FirstOrDefaultAsync(r => r.ResourceId == query.Id);

            if (resource == null)
            {
                return new PatchResourceResult
                {
                };
            }

            query.Delta.Patch(resource);

            resource.UpdateIpsFields(Enums.OperationType.Update);

            await db.SaveChangesAsync();

            return new PatchResourceResult
            {
                Resources = resource
            };
        }
    }
}

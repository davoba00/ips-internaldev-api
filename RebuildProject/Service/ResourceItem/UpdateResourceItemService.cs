using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class PatchResourceItemCommand
    {
        public Delta<ResourceItem> Delta { get; set; } = default!;
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class PatchResourceItemResult
    {
        public ResourceItem Resources { get; set; }
    }

    #endregion

    public class UpdateResourceItemService : IUpdateResourceItemService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public UpdateResourceItemService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<PatchResourceItemResult> PatchResourceItem(PatchResourceItemCommand query)
        {
            var resource = await db.ResourceItems.FirstOrDefaultAsync(r => r.ResourceItemId == query.Id);

            if (resource == null)
            {
                return new PatchResourceItemResult
                {
                };
            }

            query.Delta.Patch(resource);

            resource.UpdateIpsFields(Enums.OperationType.Create);

            await db.SaveChangesAsync();

            return new PatchResourceItemResult
            {
                Resources = resource
            };
        }

        #endregion
    }
}

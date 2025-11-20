using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class DeleteResourceItemCommand
    {
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class DeleteResourceItemResult
    {
    }

    #endregion

    public class DeleteResourceItemService : IDeleteResourceItemService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public DeleteResourceItemService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<DeleteResourceItemResult> DeleteResourceItem(DeleteResourceItemCommand query, CancellationToken cancellationToken)
        {
            var existingSql = await db.ResourceItems.FirstOrDefaultAsync(cus => cus.ResourceItemId == query.Id, cancellationToken);

            if (existingSql == null)
            {
                var result = new DeleteResourceItemResult();

                result.WithError("ResourceItem not found");

                return result;
            }

            existingSql.UpdateIpsFields(Enums.OperationType.Create);

            await db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(new DeleteResourceItemResult { });
        }

        #endregion

    }
}

using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query
    public partial class DeleteResourceItemCommand
    {
        #region Fields
        public Guid Id { get; set; }
        #endregion
    }
    #endregion

    #region Result
    public partial class DeleteResourceItemResult
    {
    }
    #endregion
    public class DeleteResourceItemService : IDeleteResourceItemService
    {
        private readonly AppDbContext db;
        public DeleteResourceItemService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<DeleteResourceItemResult> DeleteResourceItem(DeleteResourceItemCommand query)
        {
            var existingSql = await db.ResourceItems.FirstOrDefaultAsync(cus => cus.ResourceItemId == query.Id);

            if (existingSql == null) return null;

            existingSql.UpdateIpsFields(Enums.OperationType.Create);

            db.SaveChanges();

            return await Task.FromResult(new DeleteResourceItemResult { });
        }

    }
}

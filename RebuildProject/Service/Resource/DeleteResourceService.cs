using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query
    public partial class DeleteResourceCommand
    {
        #region Fields
        public Guid Id { get; set; }
        #endregion
    }
    #endregion

    #region Result
    public partial class DeleteResourceResult
    {
    }
    #endregion
    public class DeleteResourceService : IDeleteResourceService
    {
        #region Fields
        private readonly AppDbContext db;
        #endregion

        #region Contructor
        public DeleteResourceService(AppDbContext db)
        {
            this.db = db;
        }
        #endregion

        #region Public Methods
        public async Task<DeleteResourceResult> DeleteResource(DeleteResourceCommand query)
        {
            var existingSql = await db.Resources.FirstOrDefaultAsync(cus => cus.ResourceId == query.Id);

            if (existingSql == null)
            {
                return null;
            }

            existingSql.UpdateIpsFields(Enums.OperationType.Delete);

            db.SaveChanges();

            return new DeleteResourceResult { };
        }
        #endregion

    }
}

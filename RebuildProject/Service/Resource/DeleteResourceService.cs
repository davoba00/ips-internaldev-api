using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class DeleteResourceCommand
    {
        public Guid Id { get; set; }
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

        public async Task<DeleteResourceResult> DeleteResource(DeleteResourceCommand query, CancellationToken cancellationToken)
        {
            var existingSql = await db.Resources.FirstOrDefaultAsync(cus => cus.ResourceId == query.Id, cancellationToken);

            if (existingSql == null)
            {
                var result = new DeleteResourceResult();

                result.WithError("Resource not found");

                return result;
            }

            existingSql.UpdateIpsFields(Enums.OperationType.Delete);

            await db.SaveChangesAsync(cancellationToken);

            return new DeleteResourceResult { };
        }

        #endregion

    }
}

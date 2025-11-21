
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class DeleteResourceAssignmentCommand
    {
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class DeleteResourceAssingmentResult
    {
        public ResourceAssignment ResourceAssignment { get; set; }
    }

    #endregion
    public class DeleteResourceAssignmentService : IDeleteResourceAssignmentService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public DeleteResourceAssignmentService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<DeleteResourceAssingmentResult> DeleteResource(DeleteResourceAssignmentCommand query, CancellationToken cancellationToken)
        {
            var existingSql = await db.ResourceAssignments.FirstOrDefaultAsync(cus => cus.ResourceAssignmentId == query.Id, cancellationToken);

            if (existingSql == null)
            {
                var result = new DeleteResourceAssingmentResult();

                result.WithError("Resource not found");

                return result;
            }

            existingSql.UpdateIpsFields(Enums.OperationType.Delete);

            await db.SaveChangesAsync(cancellationToken);

            return new DeleteResourceAssingmentResult { };
        }

        #endregion

    }
}

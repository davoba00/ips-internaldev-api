
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.EF;

namespace RebuildProject.Service
{
    #region Query

    public partial class DeleteResourceCapacityCommand
    {
        public Guid Id { get; set; }
    }

    #endregion

    #region Result

    public partial class DeleteResourceCapacityResult
    {

    }

    #endregion

    public class DeleteResourceCapacityService : IDeleteResourceCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public DeleteResourceCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<DeleteResourceCapacityResult> DeleteResourceCapacity(DeleteResourceCapacityCommand query, CancellationToken cancellationToken)
        {
            var existingSql = await db.ResourceCapacities.FirstOrDefaultAsync(cus => cus.ResourceCapacityId == query.Id, cancellationToken);

            if (existingSql == null)
            {
                var result = new DeleteResourceCapacityResult();

                result.WithError("Resource not found");

                return result;
            }

            existingSql.UpdateIpsFields(Enums.OperationType.Delete);

            await db.SaveChangesAsync(cancellationToken);

            return new DeleteResourceCapacityResult { };
        }

        #endregion
    }
}

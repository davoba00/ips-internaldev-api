
using RebuildProject.Common;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class AddResourceCapacityCommand
    {
        public ResourceCapacity ResourceCapacity { get; set; }

    }

    #endregion

    #region Result

    public partial class AddResourceCapacityResult
    {
        public ResourceCapacity ResourceCapacity { get; set; }
    }

    #endregion

    public class AddResourceCapacityService : IAddResourceCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public AddResourceCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<AddResourceCapacityResult> AddResourceCapacity(AddResourceCapacityCommand query, CancellationToken cancellationToken)
        {
            query.ResourceCapacity.UpdateIpsFields(Enums.OperationType.Create);

            await db.ResourceCapacities.AddAsync(query.ResourceCapacity, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);

            return new AddResourceCapacityResult
            {
                ResourceCapacity = query.ResourceCapacity
            };
        }

        #endregion
    }
}

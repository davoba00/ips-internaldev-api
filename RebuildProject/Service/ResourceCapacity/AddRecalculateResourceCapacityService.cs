
using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class AddRecalculateResourceCapacityCommand
    {
        public Guid ResourceCapacityId { get; set; }
    }

    #endregion

    #region Result

    public partial class AddRecalculateResourceCapacityResult
    {
        public ResourceCapacity ResourceCapacity { get; set; }
    }

    #endregion

    public class AddRecalculateResourceCapacityService : IAddRecalculateResourceCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public AddRecalculateResourceCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<AddRecalculateResourceCapacityResult> RecalculateResourceCapacityCapacity(AddRecalculateResourceCapacityCommand query, CancellationToken cancellationToken)
        {
            var id = query.ResourceCapacityId;
            var capacity = db.ResourceCapacities.FirstOrDefault(a => a.ResourceCapacityId == id && a.Deleted == null);

            if (capacity == null)
            {
                return new AddRecalculateResourceCapacityResult
                {

                };
            }

            var resource = db.ResourceAssignments.Where(x => x.ResourceId == capacity.ResourceId && x.Deleted == null && x.DateFrom != null && x.DateTo != null);

            DateTime? minDateFrom = resource.Select(x => x.DateFrom).DefaultIfEmpty().Min();
            DateTime? maxDateTo = resource.Select(x => x.DateTo).DefaultIfEmpty().Max();

            int totalDays = 0;

            foreach (var a in resource)
            {
                totalDays += (a.DateTo.Value - a.DateFrom.Value).Days + 1;
            }

            int totalHours = totalDays * 8;

            capacity.UpdateIpsFields(Enums.OperationType.Update);
            capacity.CapacityHours = totalHours;
            capacity.DateFrom = minDateFrom;
            capacity.DateTo = maxDateTo;

            await db.SaveChangesAsync(cancellationToken);

            return new AddRecalculateResourceCapacityResult
            {
                ResourceCapacity = capacity
            };
        }

        #endregion
    }
}


using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.EF;
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

            var capacity = await db.ResourceCapacities
                            .Where(x => x.ResourceCapacityId == id && x.Deleted == null && x.Resource.Deleted == null)
                            .Include(x => x.Resource)
                            .ThenInclude(r => r.ResourceAssignments.Where(a => a.DateFrom != null && a.DateTo != null && a.Deleted == null))
                            .FirstOrDefaultAsync(cancellationToken);

            if (capacity == null || capacity?.Resource?.ResourceAssignments == null)
            {
                return new AddRecalculateResourceCapacityResult();
            }

            var assignments = capacity.Resource.ResourceAssignments;

            var minDateFrom = assignments.Min(x => x.DateFrom);
            var maxDateTo = assignments.Max(x => x.DateTo);

            int totalDays = assignments.Sum(a => WorkingDays(a.DateFrom.Value, a.DateTo.Value));

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

        #region Private Methods

        private static int WorkingDays(DateTime start, DateTime end)
        {
            int days = 0;

            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday &&
                    date.DayOfWeek != DayOfWeek.Sunday)
                {
                    days++;
                }
            }

            return days;
        }

        #endregion
    }
}

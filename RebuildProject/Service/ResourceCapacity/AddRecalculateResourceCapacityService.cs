
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
                            .Include(x => x.Resource)
                            .ThenInclude(x => x.ResourceAssignments)
                            .FirstOrDefaultAsync(x => x.ResourceCapacityId == id && x.DateFrom != null && x.DateTo != null);

            var assignments = capacity?.Resource?.ResourceAssignments;

            if (capacity == null || assignments == null)
            {
                return new AddRecalculateResourceCapacityResult();
            }

            var minDateFrom = assignments.Min(x => x.DateFrom);
            var maxDateTo = assignments.Max(x => x.DateTo);

            var workingDays = this.CalculateWorkingDays(assignments);

            var totalCapacityHours = workingDays.Count * 8;

            capacity.UpdateIpsFields(Enums.OperationType.Update);
            capacity.CapacityHours = totalCapacityHours;
            capacity.DateFrom = minDateFrom;
            capacity.DateTo = maxDateTo;

            await db.SaveChangesAsync(cancellationToken);

            return new AddRecalculateResourceCapacityResult
            {
                ResourceCapacity = capacity
            };
        }

        private List<DateTime> CalculateWorkingDays(ICollection<ResourceAssignment> assignments)
        {
            var allDates = new List<DateTime>();

            foreach (var a in assignments)
            {
                var totalDays = (a.DateTo.Value.Date - a.DateFrom.Value.Date).Days;
                foreach (var offset in Enumerable.Range(0, totalDays + 1))
                {
                    allDates.Add(a.DateFrom.Value.Date.AddDays(offset));
                }
            }

            return allDates;
        }

        #endregion

        #region Private Methods

        private int WorkingDays(DateTime start, DateTime end)
        {
            int days = 0;

            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                days++;
            }

            return days;
        }

        #endregion
    }
}


using Microsoft.EntityFrameworkCore;
using RebuildProject.Common;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class RecalculateResourceCapacityCommand
    {
        public Guid ResourceCapacityId { get; set; }
    }

    #endregion

    #region Result

    public partial class RecalculateResourceCapacityResult
    {
        public ResourceCapacity ResourceCapacity { get; set; }
    }

    #endregion

    public class RecalculateResourceCapacityService : IRecalculateResourceCapacityService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public RecalculateResourceCapacityService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<RecalculateResourceCapacityResult> RecalculateResourceCapacityCapacity(RecalculateResourceCapacityCommand query, CancellationToken cancellationToken)
        {
            var id = query.ResourceCapacityId;

            var capacity = await db.ResourceCapacities
                            .Include(x => x.Resource)
                            .ThenInclude(x => x.ResourceAssignments)
                            .FirstOrDefaultAsync(x => x.ResourceCapacityId == id && x.DateFrom != null && x.DateTo != null);

            var assignments = capacity?.Resource?.ResourceAssignments;

            if (capacity == null || assignments == null)
            {
                return new RecalculateResourceCapacityResult();
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

            return new RecalculateResourceCapacityResult
            {
                ResourceCapacity = capacity
            };
        }

        #endregion

        #region Private Methods

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
    }
}

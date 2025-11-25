
using FluentResults;
using MediatR;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetCapacityPreviewQuery
    {
        public Guid Id { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    #endregion

    #region Result

    public partial class GetCapacityPreviewResult
    {
        public ResourceCapacity Preview { get; set; }
    }

    #endregion

    public class GetCapacityPreviewService : IGetCapacityPreviewService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetCapacityPreviewService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetCapacityPreviewResult> GetCapacityPreview(GetCapacityPreviewQuery query, CancellationToken token)
        {
            var resourceId = query.Id;
            var dateFrom = query.DateFrom?.Date;
            var dateTo = query.DateTo?.Date;

            if (resourceId == Guid.Empty)
            {
                return new GetCapacityPreviewResult { };
            }

            var assignments = db.ResourceAssignments
                             .Where(a => a.ResourceId == resourceId && a.Deleted == null && a.DateFrom.HasValue && a.DateTo.HasValue);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                assignments = assignments.Where(x => x.DateFrom > dateFrom && x.DateTo < dateTo);
            }

            DateTime? minDateFrom = assignments.Select(x => x.DateFrom).DefaultIfEmpty().Min();
            DateTime? maxDateTo = assignments.Select(x => x.DateTo).DefaultIfEmpty().Max();

            var workingDays = this.CalculateWorkingDays(assignments.ToList());

            int totalHours = workingDays.Count * 8;

            var preview = new ResourceCapacity
            {
                ResourceCapacityId = Guid.NewGuid(),
                ResourceId = resourceId,
                DateFrom = minDateFrom,
                DateTo = maxDateTo,
                CapacityHours = totalHours,
            };

            return new GetCapacityPreviewResult
            {
                Preview = preview
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

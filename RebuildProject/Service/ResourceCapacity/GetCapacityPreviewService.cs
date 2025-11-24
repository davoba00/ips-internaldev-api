
using FluentResults;
using MediatR;
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

            int totalDays = assignments.ToList().Sum(a => WorkingDays(a.DateFrom.Value, a.DateTo.Value));

            int totalHours = totalDays * 8;

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

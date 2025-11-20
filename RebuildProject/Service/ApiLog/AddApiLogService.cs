using Microsoft.EntityFrameworkCore;
using RebuildProject.Models;
using System.Threading;

namespace RebuildProject.Service
{
    #region Query

    public partial class AddApiLogCommand
    {
        #region Fields

        public ApiLog ApiLog { get; set; }
        public int DaysToKeepLogEntry { get; set; }

        #endregion
    }

    #endregion

    #region Result

    public partial class AddApiLogResult
    {
        public ApiLog ApiLog { get; set; }
    }

    #endregion

    public class AddApiLogService : IAddApiLogService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public AddApiLogService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<AddApiLogResult> AddApiLog(AddApiLogCommand query, CancellationToken cancellationToken)
        {
            await this.db.ApiLogs.AddAsync(query.ApiLog, cancellationToken);

            await this.db.SaveChangesAsync(cancellationToken);

            await this.CleanupAsync(query.DaysToKeepLogEntry);

            return new AddApiLogResult
            {
                ApiLog = query.ApiLog
            };
        }

        #endregion

        #region Private Methods

        private async Task CleanupAsync(int DaysToKeepLogEntry)
        {
            if (DaysToKeepLogEntry <= 0)
            {
                return;
            }

            var cutoff = DateTime.Now.AddDays(-DaysToKeepLogEntry);

            await this.db.ApiLogs.Where(x => x.RequestTime < cutoff).ExecuteDeleteAsync();
        }

        #endregion
    }
}

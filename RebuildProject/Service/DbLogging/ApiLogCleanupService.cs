using Microsoft.Extensions.Options;
using RebuildProject.Models;

namespace RebuildProject.Service.DbLogging
{
    public interface IApiLogCleanupService
    {
        Task CleanupAsync();
    }
    public class ApiLogCleanupService : IApiLogCleanupService
    {
        private readonly AppDbContext db;
        private readonly DbLoggingSettings settings;

        public ApiLogCleanupService(AppDbContext db, IOptions<DbLoggingSettings> options)
        {
            this.db = db;
            this.settings = options.Value;
        }
        public async Task CleanupAsync()
        {
            if (!settings.Enable)
                return;

            if (settings.DaysToKeepLogEntry <= 0)
                return;

            var cutoff = DateTime.UtcNow.AddDays(-settings.DaysToKeepLogEntry);

            var oldLogs = db.ApiLogs.Where(x => x.RequestTime < cutoff);

            db.ApiLogs.RemoveRange(oldLogs);

            await db.SaveChangesAsync();
        }
    }
}

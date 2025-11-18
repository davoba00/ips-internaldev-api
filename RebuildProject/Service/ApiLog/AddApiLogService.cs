using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query
    public partial class AddApiLogCommand
    {
        #region Fields
        public ApiLog ApiLog { get; set; }
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
        public async Task<AddApiLogResult> AddApiLog(AddApiLogCommand query)
        {

            await db.ApiLogs.AddAsync(query.ApiLog);
            await db.SaveChangesAsync();

            return new AddApiLogResult
            {
                ApiLog = query.ApiLog
            };
        }
        #endregion
    }
}

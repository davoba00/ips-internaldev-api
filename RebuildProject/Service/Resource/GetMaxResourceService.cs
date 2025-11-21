using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetMaxResourceQuery : IRequest<GetMaxResourceResult> { }

    #endregion

    #region Result

    public partial class GetMaxResourceResult : Result
    {
        public IQueryable<Resource> Resource { get; set; }
    }

    #endregion

    public class GetMaxResourceService : IGetMaxResourcesService
    {
        #region Fields

        private readonly AppDbContext db;
        private readonly ILogger<AddResourceService> logger;

        #endregion

        #region Contructor

        public GetMaxResourceService(AppDbContext db, ILogger<AddResourceService> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        #endregion
        public async Task<GetMaxResourceResult> GetMaxResource(GetMaxResourceQuery query, CancellationToken token)
        {
            var maxResource = await db.Resources.Where(x => x.Deleted == null).OrderByDescending(x => x.Created).FirstOrDefaultAsync();

            return new GetMaxResourceResult
            {
                Resource = new[] { maxResource }.AsQueryable()
            };
        }
    }
}

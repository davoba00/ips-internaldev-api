
using Microsoft.AspNetCore.OData.Query;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query
    public partial class GetResourceQuery
    {
        #region Fields
        public ODataQueryOptions<Resource> QueryOptions { get; set; }
        public Guid Id { get; set; }
        #endregion
    }
    #endregion

    #region Result
    public partial class GetResourceResult
    {
        #region Fields
        public IQueryable<Resource> Resource { get; set; }
        #endregion

    }
    #endregion

    public class GetResourceService : IGetResourceService
    {
        #region Fields
        private readonly AppDbContext db;
        #endregion

        #region Constructor
        public GetResourceService(AppDbContext db)
        {
            this.db = db;
        }
        #endregion

        #region Public Methods
        public async Task<GetResourceResult> GetResource(GetResourceQuery query)
        {
            var dataSql = db.Resources.Where(x => x.ResourceId == query.Id && x.Deleted == null);

            return await Task.FromResult(new GetResourceResult
            {
                Resource = dataSql
            });
        }
        #endregion
    }
}

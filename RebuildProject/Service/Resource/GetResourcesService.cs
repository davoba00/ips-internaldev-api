
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class GetResourcesQuery
    {
        public ODataQueryOptions<Resource> QueryOptions { get; set; }
    }

    #endregion

    #region Result

    public partial class GetResourcesResult
    {
        public IQueryable<Resource> Resources { get; set; }
    }

    #endregion


    public class GetResourcesService : IGetResourcesService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Constructor

        public GetResourcesService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<GetResourcesResult> GetResources(GetResourcesQuery query)
        {
            var list = db.Resources.AsQueryable();

            return await Task.FromResult(new GetResourcesResult
            {
                Resources = list
            });
        }

        #endregion
    }
}

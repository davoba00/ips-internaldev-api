using RebuildProject.Common;
using RebuildProject.EF;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class AddResourceCommand
    {
        public Resource Resource { get; set; }

    }

    #endregion

    #region Result

    public partial class AddResourceResult
    {
        public Resource Resource { get; set; }
    }

    #endregion

    public class AddResourceService : IAddResourceService
    {
        #region Fields

        private readonly AppDbContext db;
        private readonly ILogger<AddResourceService> logger;

        #endregion

        #region Contructor

        public AddResourceService(AppDbContext db, ILogger<AddResourceService> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<AddResourceResult> AddResource(AddResourceCommand query, CancellationToken cancellationToken)
        {
            query.Resource.UpdateIpsFields(Enums.OperationType.Create);

            await db.Resources.AddAsync(query.Resource, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);

            return new AddResourceResult
            {
                Resource = query.Resource
            };
        }

        #endregion
    }
}

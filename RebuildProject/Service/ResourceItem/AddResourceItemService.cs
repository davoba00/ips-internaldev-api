using MediatR;
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class AddResourceItemCommand : IRequest<AddResourceItemResult>
    {
        public ResourceItem ResourceItem { get; set; }
    }

    #endregion

    #region Result

    public partial class AddResourceItemResult
    {
        public ResourceItem Resource { get; set; }
    }

    #endregion

    public class AddResourceItemService : IAddResourceItemService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public AddResourceItemService(AppDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<AddResourceItemResult> AddResourceItem(AddResourceItemCommand query)
        {
            query.ResourceItem.UpdateIpsFields(Enums.OperationType.Create);

            await db.ResourceItems.AddAsync(query.ResourceItem);
            await db.SaveChangesAsync();

            return new AddResourceItemResult
            {
                Resource = query.ResourceItem
            };
        }

        #endregion
    }
}

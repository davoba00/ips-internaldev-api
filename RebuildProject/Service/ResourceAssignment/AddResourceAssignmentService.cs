
using RebuildProject.Common;
using RebuildProject.Models;

namespace RebuildProject.Service
{
    #region Query

    public partial class AddResourceAssignmentCommand
    {
        public ResourceAssignment ResourceAssignment { get; set; }

    }

    #endregion

    #region Result

    public partial class AddResourceAssignmentResult
    {
        public ResourceAssignment ResourceAssignment { get; set; }
    }

    #endregion

    public class AddResourceAssignmentService : IAddResourceAssignmentService
    {
        #region Fields

        private readonly AppDbContext db;

        #endregion

        #region Contructor

        public AddResourceAssignmentService(AppDbContext db )
        {
            this.db = db;
        }

        #endregion

        #region Public Methods

        public async Task<AddResourceAssignmentResult> AddResourceAssignment(AddResourceAssignmentCommand query, CancellationToken cancellationToken)
        {
            query.ResourceAssignment.UpdateIpsFields(Enums.OperationType.Create);

            await db.ResourceAssignments.AddAsync(query.ResourceAssignment, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);

            return new AddResourceAssignmentResult
            {
                ResourceAssignment = query.ResourceAssignment
            };
        }

        #endregion
    }
}

using RebuildProject.Common;

namespace RebuildProject.Models;

public partial class ResourceAssignment
{
    public Guid ResourceAssignmentId { get; set; }

    public Guid ResourceId { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    [IpsField(Enums.FieldNames.DateCreated)]
    public DateTime? Created { get; set; }

    [IpsField(Enums.FieldNames.DateUpdated)]
    public DateTime? Updated { get; set; }

    [IpsField(Enums.FieldNames.DateDeleted)]
    public DateTime? Deleted { get; set; }
    public virtual Resource Resource { get; set; }
}

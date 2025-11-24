using RebuildProject.Common;

namespace RebuildProject.Models;

public partial class Resource
{
    public Resource()
    {
        this.ResourceAssignments = new HashSet<ResourceAssignment>();
        this.ResourceItems = new HashSet<ResourceItem>();
        this.ResourceCapacities = new HashSet<ResourceCapacity>();
    }
    public Guid ResourceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [IpsField(Enums.FieldNames.DateCreated)]
    public DateTime? Created { get; set; }

    [IpsField(Enums.FieldNames.DateUpdated)]
    public DateTime? Updated { get; set; }

    [IpsField(Enums.FieldNames.DateDeleted)]
    public DateTime? Deleted { get; set; }

    [IpsField(Enums.FieldNames.MachineNameCreated)]
    public string MachineNameCreated { get; set; }

    [IpsField(Enums.FieldNames.MachineNameUpdated)]
    public string MachineNameUpdated { get; set; }

    public virtual ICollection<ResourceItem> ResourceItems { get; set; }
    public virtual ICollection<ResourceAssignment> ResourceAssignments { get; set; }
    public virtual ICollection<ResourceCapacity> ResourceCapacities { get; set; }
}

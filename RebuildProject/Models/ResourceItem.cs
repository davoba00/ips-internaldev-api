using RebuildProject.Common;
using System;
using System.Collections.Generic;

namespace RebuildProject.Models;

public partial class ResourceItem
{
    public Guid ResourceItemId { get; set; }

    public Guid? ResourceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    [IpsField(Enums.FieldNames.DateCreated)]
    public DateTime? Created { get; set; }

    [IpsField(Enums.FieldNames.DateUpdated)]
    public DateTime? Updated { get; set; }

    [IpsField(Enums.FieldNames.DateDeleted)]
    public DateTime? Deleted { get; set; }

    [IpsField(Enums.FieldNames.MachineNameCreated)]
    public string? MachineNameCreated { get; set; }

    [IpsField(Enums.FieldNames.MachineNameUpdated)]
    public string? MachineNameUpdated { get; set; }

    public virtual Resource? Resource { get; set; }
}

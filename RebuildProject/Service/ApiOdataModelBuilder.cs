using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using RebuildProject.Models;

namespace ODataResourceApi.Services.OData
{
    public static class ApiODataModelBuilder
    {
        #region Public Methods

        public static IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();

            #region EntitySet

            odataBuilder.EntitySet<Resource>("resource").EntityType.HasKey(r => r.ResourceId);

            odataBuilder.EntitySet<ResourceItem>("resourceitem").EntityType.HasKey(ri => ri.ResourceItemId);

            odataBuilder.EntitySet<ApiLog>("apilog").EntityType.HasKey(ri => ri.LogId);

            odataBuilder.EntitySet<ResourceAssignment>("resourceAssignment").EntityType.HasKey(ri => ri.ResourceAssignmentId);

            odataBuilder.EntitySet<ResourceCapacity>("resourceCapacity").EntityType.HasKey(ri => ri.ResourceCapacityId);

            #endregion

            #region Unbound Function or Action

            odataBuilder.Function("getAllResource").ReturnsFromEntitySet<Resource>("resource");

            odataBuilder.Function("mostRecent").ReturnsFromEntitySet<Resource>("resource");

            odataBuilder.Action("incrementResource").ReturnsFromEntitySet<Resource>("resource");

            #endregion

            return odataBuilder.GetEdmModel();
        }

        #endregion
    }
}

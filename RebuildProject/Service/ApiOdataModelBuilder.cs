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

            var resource = odataBuilder.EntitySet<Resource>("resource")
                .EntityType.HasKey(r => r.ResourceId);

            var resourceItem = odataBuilder.EntitySet<ResourceItem>("resourceitem")
                .EntityType.HasKey(ri => ri.ResourceItemId);

            var apiLog = odataBuilder.EntitySet<ApiLog>("apilog")
                .EntityType.HasKey(ri => ri.LogId);


            return odataBuilder.GetEdmModel();
        }

        #endregion
    }
}

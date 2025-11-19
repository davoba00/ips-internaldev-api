using System.Net.NetworkInformation;
using static RebuildProject.Common.Enums;

namespace RebuildProject.Common
{
    public static class UtilityExtensions
    {
        public static void UpdateIpsFields<TEntity>(this TEntity entity, OperationType operationType)
        {
            IPGlobalProperties globalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var properties = typeof(TEntity).GetProperties();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttributes(typeof(IpsFieldAttribute), inherit: true)
                               .FirstOrDefault() as IpsFieldAttribute;

                if (attr == null) continue;

                switch (operationType)
                {
                    case OperationType.Create:
                        if (attr.FieldName == FieldNames.DateCreated)
                        {
                            prop.SetValue(entity, DateTime.Now);
                        }
                        if (attr.FieldName == FieldNames.MachineNameCreated)
                        {
                            prop.SetValue(entity, globalProperties.HostName);
                        }
                        break;

                    case OperationType.Update:
                        if (attr.FieldName == FieldNames.DateUpdated)
                        {
                            prop.SetValue(entity, DateTime.Now);
                        }
                        if (attr.FieldName == FieldNames.MachineNameUpdated )
                        {
                            prop.SetValue(entity, globalProperties.HostName);
                        }
                        break;

                    case OperationType.Delete:
                        if (attr.FieldName == FieldNames.DateDeleted)
                        {
                            prop.SetValue(entity, DateTime.Now);
                        }
                        if (attr.FieldName == FieldNames.MachineNameUpdated)
                        {
                            prop.SetValue(entity, globalProperties.HostName);
                        }
                        break;
                }
            }
        }
    }
}

using static RebuildProject.Common.Enums;

namespace RebuildProject.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IpsFieldAttribute : Attribute
    {
        public FieldNames FieldName { get; }

        public IpsFieldAttribute(FieldNames fieldName)
        {
            this.FieldName = fieldName;
        }
    }
}
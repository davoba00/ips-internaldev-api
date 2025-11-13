namespace RebuildProject.Common
{
    public class Enums
    {
        public enum OperationType
        {
            Create,
            Update,
            Delete
        }

        public enum FieldNames
        {
            None,
            DateCreated,
            DateUpdated,
            DateDeleted,
            MachineNameCreated,
            MachineNameUpdated
        }

        public enum ErrorMetadataKey
        {
            Status
        }
    }
}

using Dapper;

namespace Application.Library
{
    public static class DatabaseModels
    {
        public enum DmlType
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public enum EntityType
        {
            Enterprise = 1,
            PersonPhysical = 2,
            PersonJuridical = 3,
            Document = 4,
            Contact = 5,
            Address = 6,
            Employee = 7,
            Client = 8,
            User = 9
        }

        public class ControlDto
        {
            public DmlType DmlTypeId { get; set; }
            public EntityType EntityTypeId { get; set; }
            public int EntityId { get; set; }
            public int UserId { get; set; }
            public DateTime ControlDate { get; set; }
            public int EnterpreiseId { get; set; }
        }

        public class BancoCommitArgument
        {
            public DmlType Control { get; set; }
            public EntityType Entity { get; set; }
            public int EntityId { get; set; }
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }

        public class BancoArgument
        {
            public string Sql { get; set; } = string.Empty;
            public DynamicParameters Parameter { get; set; } = new DynamicParameters();
            public int CmdType { get; set; }
        }

        public class BancoExecuteArgument
        {
            public string Sql { get; set; } = string.Empty;
            public DynamicParameters Parameter { get; set; } = new DynamicParameters();
            public int CmdType { get; set; }
            public string Output { get; set; } = string.Empty;
        }
    }
}

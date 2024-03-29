﻿using System.Data;

namespace Application.Base.Models;

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

    public class BancoCommitArgument<T>
    {
        public DmlType Control { get; set; }
        public EntityType Entity { get; set; }
        public T EntityId { get; set; } =  default(T);
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
    }

    public class Parameter
    {
        public string Field { get; set; } = string.Empty;
        public object? Value { get; set; }
        public ParameterDirection Direction { get; set; }
    }

    public class ParameterCollection
    {
        public List<Parameter> parameters { get; } = new List<Parameter>();

        public static ParameterCollection GetInstance() => new ParameterCollection();
        public ParameterCollection Add(string field, object? value)
            => Add(field, value, ParameterDirection.Input);
        public ParameterCollection Add(string field, object? value, ParameterDirection direction)
        {
            this.parameters.Add(new Parameter { Field = field, Value = value, Direction = direction });
            return this;
        }
    }

    public class BancoArgument
    {
        public string Sql { get; set; } = string.Empty;
        public ParameterCollection Parameter { get; set; } = new ParameterCollection();
        public int CmdType { get; set; }
    }

    public class BancoExecuteArgument
    {
        public string Sql { get; set; } = string.Empty;
        public ParameterCollection Parameter { get; set; } = new ParameterCollection();
        public int CmdType { get; set; }
    }

    public class BancoExecuteScalarArgument
    {
        public string Sql { get; set; } = string.Empty;
        public ParameterCollection Parameter { get; set; } = new ParameterCollection();
        public int CmdType { get; set; }
        public string Output { get; set; } = string.Empty;
    }
}

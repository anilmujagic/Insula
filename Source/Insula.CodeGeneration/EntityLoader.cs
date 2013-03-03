using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Insula.Data.DatabaseServices;

namespace Insula.CodeGeneration
{
    public static class EntityLoader
    {
        public static EntityCollection LoadFromDatabase(string connectionString)
        {
            var entities = new EntityCollection();

            var schemaInfo = DatabaseSchemaInfo.LoadFromDatabase(connectionString);
            foreach (var table in schemaInfo.Tables)
            {
                var entity = new Entity
                {
                    Name = table.Name,
                    Schema = table.Schema,
                    Database = table.Database
                };
                entities.Add(entity);

                foreach (var column in table.Columns)
                {
                    var member = new EntityMember
                    {
                        Name = column.Name,
                        DataType = DataTypeInfo.CreateFromSqlDataTypeName(column.DataType, column.IsNullable),
                        MaxLength = column.MaxLength,
                        DecimalPlaces = column.DecimalPlaces,
                        IsNullable = column.IsNullable,
                        IsPrimaryKey = column.IsPrimaryKey,
                        IsIdentity = column.IsIdentity,
                        IsComputed = column.IsComputed,
                        OrdinalPosition = column.OrdinalPosition
                    };
                    entity.Members.Add(member);
                }
            }

            return entities;
        }
    }
}

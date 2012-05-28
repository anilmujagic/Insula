using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Insula.CodeGeneration
{
    public class SystemDataTypeInfo
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }

        private SystemDataTypeInfo()
        {
        }

        private void SetDataTypeInfo(Type type, string cSharpAlias, bool isNullableValueType)
        {
            this.SetDataTypeInfo(type.FullName, cSharpAlias, isNullableValueType);
        }

        private void SetDataTypeInfo(string name, string alias, bool isNullableValueType)
        {
            if (isNullableValueType)
            {
                name += "?";
                alias += "?";
            }

            this.Name = name;
            this.Alias = alias;
        }

        public static SystemDataTypeInfo CreateFromSqlDataTypeName(string sqlServerDataTypeName, bool isNullable)
        {
            var dataTypeInfo = new SystemDataTypeInfo();
            dataTypeInfo.SetDataTypeInfo(typeof(object), "object", isNullable && false);

            switch (sqlServerDataTypeName.ToUpperInvariant())
            {
                //Textual
                case "CHAR":
                case "NCHAR":
                case "VARCHAR":
                case "NVARCHAR":
                case "TEXT":
                case "NTEXT":
                case "XML":
                    dataTypeInfo.SetDataTypeInfo(typeof(string), "string", isNullable && false);
                    break;
                //Integers
                case "BIGINT":
                    dataTypeInfo.SetDataTypeInfo(typeof(long), "long", isNullable && true);
                    break;
                case "INT":
                    dataTypeInfo.SetDataTypeInfo(typeof(int), "int", isNullable && true);
                    break;
                case "SMALLINT":
                    dataTypeInfo.SetDataTypeInfo(typeof(short), "short", isNullable && true);
                    break;
                case "TINYINT":
                    dataTypeInfo.SetDataTypeInfo(typeof(byte), "byte", isNullable && true);
                    break;
                //Boolean
                case "BIT":
                    dataTypeInfo.SetDataTypeInfo(typeof(bool), "bool", isNullable && true);
                    break;
                //Decimal
                case "NUMERIC":
                case "DECIMAL":
                case "MONEY":
                case "SMALLMONEY":
                    dataTypeInfo.SetDataTypeInfo(typeof(decimal), "decimal", isNullable && true);
                    break;
                //Floating point
                case "FLOAT":
                    dataTypeInfo.SetDataTypeInfo(typeof(double), "double", isNullable && true);
                    break;
                case "REAL":
                    dataTypeInfo.SetDataTypeInfo(typeof(float), "float", isNullable && true);
                    break;
                //Date and time
                case "DATE":
                case "DATETIME":
                case "DATETIME2":
                case "SMALLDATETIME":
                    dataTypeInfo.SetDataTypeInfo(typeof(DateTime), "DateTime", isNullable && true);
                    break;
                case "DATETIMEOFFSET":
                    dataTypeInfo.SetDataTypeInfo(typeof(DateTimeOffset), "DateTimeOffset", isNullable && true);
                    break;
                case "TIME":
                    dataTypeInfo.SetDataTypeInfo(typeof(TimeSpan), "TimeSpan", isNullable && true);
                    break;
                //Binary
                case "IMAGE":
                case "BINARY":
                case "VARBINARY":
                case "TIMESTAMP":
                case "ROWVERSION":
                    dataTypeInfo.SetDataTypeInfo(typeof(byte[]), "byte[]", isNullable && false);
                    break;
                //GUID
                case "UNIQUEIDENTIFIER":
                    dataTypeInfo.SetDataTypeInfo(typeof(Guid), "Guid", isNullable && true);
                    break;
                //Geo
                case "GEOGRAPHY":
                    dataTypeInfo.SetDataTypeInfo("Microsoft.SqlServer.Types.SqlGeography", "Microsoft.SqlServer.Types.SqlGeography", isNullable && false);
                    break;
                case "GEOMETRY":
                    dataTypeInfo.SetDataTypeInfo("Microsoft.SqlServer.Types.SqlGeometry", "Microsoft.SqlServer.Types.SqlGeometry", isNullable && false);
                    break;
            }

            return dataTypeInfo;
        }
    }
}

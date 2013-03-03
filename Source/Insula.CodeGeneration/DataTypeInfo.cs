using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Insula.CodeGeneration
{
    public class DataTypeInfo
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public string SqlDataTypeName { get; private set; }

        private DataTypeInfo()
        {
        }

        private void SetDataTypeInfo(Type type, string cSharpAlias, string sqlDataTypeName, bool isNullableValueType)
        {
            this.SetDataTypeInfo(type.FullName, cSharpAlias, sqlDataTypeName, isNullableValueType);
        }

        private void SetDataTypeInfo(string name, string alias, string sqlDataTypeName, bool isNullableValueType)
        {
            if (isNullableValueType)
            {
                name += "?";
                alias += "?";
            }

            this.Name = name;
            this.Alias = alias;
            this.SqlDataTypeName = sqlDataTypeName;
        }

        public static DataTypeInfo CreateFromSqlDataTypeName(string sqlDataTypeName, bool isNullable)
        {
            var dataTypeInfo = new DataTypeInfo();
            dataTypeInfo.SetDataTypeInfo(typeof(object), "object", string.Empty, isNullable && false);

            sqlDataTypeName = sqlDataTypeName.ToUpperInvariant();
            switch (sqlDataTypeName)
            {
                //Textual
                case "CHAR":
                case "NCHAR":
                case "VARCHAR":
                case "NVARCHAR":
                case "TEXT":
                case "NTEXT":
                case "XML":
                    dataTypeInfo.SetDataTypeInfo(typeof(string), "string", sqlDataTypeName, isNullable && false);
                    break;
                //Integers
                case "BIGINT":
                    dataTypeInfo.SetDataTypeInfo(typeof(long), "long", sqlDataTypeName, isNullable && true);
                    break;
                case "INT":
                    dataTypeInfo.SetDataTypeInfo(typeof(int), "int", sqlDataTypeName, isNullable && true);
                    break;
                case "SMALLINT":
                    dataTypeInfo.SetDataTypeInfo(typeof(short), "short", sqlDataTypeName, isNullable && true);
                    break;
                case "TINYINT":
                    dataTypeInfo.SetDataTypeInfo(typeof(byte), "byte", sqlDataTypeName, isNullable && true);
                    break;
                //Boolean
                case "BIT":
                    dataTypeInfo.SetDataTypeInfo(typeof(bool), "bool", sqlDataTypeName, isNullable && true);
                    break;
                //Decimal
                case "NUMERIC":
                case "DECIMAL":
                case "MONEY":
                case "SMALLMONEY":
                    dataTypeInfo.SetDataTypeInfo(typeof(decimal), "decimal", sqlDataTypeName, isNullable && true);
                    break;
                //Floating point
                case "FLOAT":
                    dataTypeInfo.SetDataTypeInfo(typeof(double), "double", sqlDataTypeName, isNullable && true);
                    break;
                case "REAL":
                    dataTypeInfo.SetDataTypeInfo(typeof(float), "float", sqlDataTypeName, isNullable && true);
                    break;
                //Date and time
                case "DATE":
                case "DATETIME":
                case "DATETIME2":
                case "SMALLDATETIME":
                    dataTypeInfo.SetDataTypeInfo(typeof(DateTime), "DateTime", sqlDataTypeName, isNullable && true);
                    break;
                case "DATETIMEOFFSET":
                    dataTypeInfo.SetDataTypeInfo(typeof(DateTimeOffset), "DateTimeOffset", sqlDataTypeName, isNullable && true);
                    break;
                case "TIME":
                    dataTypeInfo.SetDataTypeInfo(typeof(TimeSpan), "TimeSpan", sqlDataTypeName, isNullable && true);
                    break;
                //Binary
                case "IMAGE":
                case "BINARY":
                case "VARBINARY":
                case "TIMESTAMP":
                case "ROWVERSION":
                    dataTypeInfo.SetDataTypeInfo(typeof(byte[]), "byte[]", sqlDataTypeName, isNullable && false);
                    break;
                //GUID
                case "UNIQUEIDENTIFIER":
                    dataTypeInfo.SetDataTypeInfo(typeof(Guid), "Guid", sqlDataTypeName, isNullable && true);
                    break;
                //Geo
                case "GEOGRAPHY":
                    dataTypeInfo.SetDataTypeInfo("Microsoft.SqlServer.Types.SqlGeography", "Microsoft.SqlServer.Types.SqlGeography", sqlDataTypeName, isNullable && false);
                    break;
                case "GEOMETRY":
                    dataTypeInfo.SetDataTypeInfo("Microsoft.SqlServer.Types.SqlGeometry", "Microsoft.SqlServer.Types.SqlGeometry", sqlDataTypeName, isNullable && false);
                    break;
            }

            return dataTypeInfo;
        }
    }
}

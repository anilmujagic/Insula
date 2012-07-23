using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Insula.Common;

namespace Insula.Data.Orm
{
    internal class ColumnMetadata
    {
        public PropertyInfo PropertyInfo { get; private set; }
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public bool IsNullable { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsIdentity { get; private set; }
        public bool IsMapped { get; private set; }

        public ColumnMetadata(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.Name = propertyInfo.Name;
            this.Type = propertyInfo.PropertyType;

            var attributes = propertyInfo.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), true);
            this.IsNullable = attributes.IsNullOrEmpty();

            attributes = propertyInfo.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true);
            this.IsPrimaryKey = !attributes.IsNullOrEmpty();

            attributes = propertyInfo.GetCustomAttributes(typeof(Insula.DataAnnotations.IdentityAttribute), true);
            this.IsIdentity = !attributes.IsNullOrEmpty();

            attributes = propertyInfo.GetCustomAttributes(typeof(Insula.DataAnnotations.MappedAttribute), true);
            this.IsMapped = !attributes.IsNullOrEmpty();
        }
    }
}

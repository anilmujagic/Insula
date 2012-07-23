using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Insula.CodeGeneration
{
    public class EntityMember
    {
        public string Name { get; set; }
        public DataTypeInfo DataType { get; set; }
        public int? MaxLength { get; set; }
        public int? DecimalPlaces { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }
        public int OrdinalPosition { get; set; }
        public Collection<string> Attributes { get; private set; }

        public EntityMember()
        {
            this.Attributes = new Collection<string>();
        }
    }
}

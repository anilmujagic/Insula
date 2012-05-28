using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insula.Common;

namespace Insula.Data.Orm
{
    internal class TableMetadata
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }

        private List<ColumnMetadata> _columns;
        public IEnumerable<ColumnMetadata> Columns
        {
            get
            {
                return _columns.AsReadOnly();
            }
        }

        public TableMetadata(Type type)
        {
            if (!type.IsClass)
                throw new ArgumentException("Parameter must be a class.", "type");

            this.Name = type.Name;
            this.Type = type;

            _columns = new List<ColumnMetadata>();

            var props = type.GetProperties();
            foreach (var p in props)
            {
                _columns.Add(new ColumnMetadata(p));
            }
        }

        //TODO: Make read-only property
        public ColumnMetadata GetIdentityColumn()
        {
            return this.Columns
                .SingleOrDefault(c => c.IsMapped && c.IsIdentity);
        }

        //TODO: Make field backed read-only property for performance reasons
        public IEnumerable<ColumnMetadata> GetKeyColumns()
        {
            return this.Columns
                .Where(c => c.IsMapped && c.IsPrimaryKey);
        }

        //TODO: Make field backed read-only property for performance reasons
        public IEnumerable<ColumnMetadata> GetInsertColumns()
        {
            return this.Columns
                .Where(c => c.IsMapped && !c.IsIdentity)
                .OrderByDescending(c => c.IsPrimaryKey);
        }

        //TODO: Make field backed read-only property for performance reasons
        public IEnumerable<ColumnMetadata> GetUpdateColumns()
        {
            return this.Columns
                .Where(c => c.IsMapped && !c.IsPrimaryKey && !c.IsIdentity);
        }

        //TODO: Make field backed read-only property for performance reasons
        public IEnumerable<ColumnMetadata> GetSelectColumns()
        {
            return this.Columns
                .Where(c => c.IsMapped)
                .OrderByDescending(c => c.IsPrimaryKey);
        }
    }
}

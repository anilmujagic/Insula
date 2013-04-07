using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Insula.Common;
using System.Globalization;

namespace Insula.Data.Orm
{
    public class SqlQuery<T> where T : class, new()
    {
        private readonly Repository<T> _repository;
        private string _columnNames;
        private string _where;
        private object[] _parameters;
        private string[] _orderByColumns;
        private long _skip;
        private long _take = -1;  //-1 means no limit

        private readonly Dictionary<string, TableMetadata> _joins;
        private readonly Dictionary<string, object> _joinMaterializers;

        internal SqlQuery(Repository<T> repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;
            _joins = new Dictionary<string, TableMetadata>();
            _joinMaterializers = new Dictionary<string, object>();
        }

        public SqlQuery<T> Include(string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "Type {0} doesn't have a property named {1}", typeof(T).Name, propertyName),
                    "propertyName");

            var tableMetadata = new TableMetadata(property.PropertyType);
            _joins.Add(propertyName, tableMetadata);

            var materializerType = typeof(Materializer<>).MakeGenericType(new Type[] { tableMetadata.Type });
            var materializer = Activator.CreateInstance(materializerType, tableMetadata, propertyName);
            _joinMaterializers.Add(propertyName, materializer);

            return this;
        }

        public SqlQuery<T> Where(string whereClause, params object[] parameters)
        {
            if (!_where.IsNullOrWhiteSpace())
                throw new InvalidOperationException("WHERE clause is already set.");

            _where = whereClause;
            _parameters = parameters;
            return this;
        }

        /// <summary>
        /// Creates a SQL WHERE clause by extracting names and values from properties of passed object.
        /// </summary>
        /// <param name="columnValueFilters">Anonymous or typed object. Example: <c>Where(new { ItemCategoryID = "FOOD", IsAvailable = true })</c></param>
        public SqlQuery<T> Where(object columnValueFilters, bool includePropertiesHavingDefaultTypeValue = false)
        {
            if (columnValueFilters == null)
                throw new ArgumentNullException("columnValueFilters");

            if (!_where.IsNullOrWhiteSpace())
                throw new InvalidOperationException("WHERE clause is already set.");

            var columns = new List<Tuple<int, string, object>>();

            var properties = columnValueFilters.GetType().GetProperties();

            if (properties.Length > 0)
            {
                var position = 0;
                foreach (var p in properties)
                {
                    var type = p.PropertyType;
                    var defaultValue = type.IsValueType ? Activator.CreateInstance(type) : null;
                    var value = p.GetValue(columnValueFilters, null);
                    if (!(value.Equals(defaultValue) && !includePropertiesHavingDefaultTypeValue))
                    {
                        columns.Add(new Tuple<int, string, object>(position, p.Name, value));
                        position++;
                    }
                }

                _where = string.Join(" AND ", columns
                    .Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}] = @{1}", c.Item2, c.Item1)));

                _parameters = columns
                    .Select(c => c.Item3)
                    .ToArray();
            }

            return this;
        }

        public SqlQuery<T> OrderBy(params string[] columns)
        {
            if (!_orderByColumns.IsNullOrEmpty())
                throw new InvalidOperationException("ORDER BY clause is already set.");

            _orderByColumns = columns;
            return this;
        }

        public IEnumerable<T> GetAll()
        {
            using (var command = _repository.DatabaseContext.CreateCommand(this.ParseQuery(), _parameters))
            {
                var entities = new List<T>();
                var fkEntities = new Dictionary<Type, Dictionary<string, object>>();

                _repository.DatabaseContext.OpenConnection();

                SqlDataReader reader;
                try
                {
                    reader = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _repository.DatabaseContext.CloseConnection();
                    _repository.DatabaseContext.OnException(ex, command.CommandText);
                    throw;
                }

                var useAliases = !_joins.IsNullOrEmpty();

                while (reader.Read())
                {
                    var entity = _repository.Materializer.Materialize(reader, useAliases);

                    foreach (var join in _joins)
                    {
                        #region Needs optimisation

                        var mat = _joinMaterializers[join.Key];
                        object fkEntity = mat.GetType().GetMethod("Materialize").Invoke(mat, new object[] { reader, useAliases });

                        ////Alternative:
                        //dynamic mat = _joinMaterializers[join.Key];
                        //object fkEntity = mat.Materialize(reader, useAliases);

                        if (fkEntity == null)
                            continue;

                        if (!fkEntities.ContainsKey(join.Value.Type))
                            fkEntities.Add(join.Value.Type, new Dictionary<string, object>());

                        var keyValues = string.Join("|", join.Value.KeyColumns
                            .Select(c => fkEntity.GetType().GetProperty(c.Name).GetValue(fkEntity, null).ToString()));

                        //Making sure no multiple instances of the same entity are created
                        if (fkEntities[join.Value.Type].ContainsKey(keyValues))
                            fkEntity = fkEntities[join.Value.Type][keyValues];
                        else
                            fkEntities[join.Value.Type].Add(keyValues, fkEntity);

                        entity.GetType().GetProperty(join.Key).SetValue(entity, fkEntity, null);

                        #endregion
                    }

                    entities.Add(entity);
                }

                reader.Dispose();

                _repository.DatabaseContext.CloseConnection();

                return entities;
            }
        }

        public IEnumerable<T> GetSubset(long skip, long take)
        {
            if (skip < 0)
                throw new ArgumentOutOfRangeException("skip", skip, "Parameter must be greather than or equal to zero.");
            if (take < 0)
                throw new ArgumentOutOfRangeException("take", take, "Parameter must be greather than or equal to zero.");

            _skip = skip;
            _take = take;

            return this.GetAll();
        }

        public IEnumerable<T> GetTop(long top)
        {
            if (top < 0)
                throw new ArgumentOutOfRangeException("top", top, "Parameter must be greather than or equal to zero.");

            return this.GetSubset(0, top);
        }

        public T GetFirst()
        {
            return this.GetTop(1).FirstOrDefault();
        }

        public T GetSingle()
        {
            return this.GetTop(2).SingleOrDefault();
        }

        public int GetCount()
        {
            _columnNames = "COUNT(*)";

            var count = (int?)_repository.DatabaseContext.ExecuteScalar(this.ParseQuery(), _parameters);
            return count ?? 0;
        }

        public long GetLongCount()
        {
            _columnNames = "COUNT_BIG(*)";

            var count = (long?)_repository.DatabaseContext.ExecuteScalar(this.ParseQuery(), _parameters);
            return count ?? 0;
        }

        private string ParseQuery()
        {
            string orderByColumns = string.Empty;
            var joins = new StringBuilder();

            if (_joins.IsNullOrEmpty())
            {
                if (_columnNames.IsNullOrWhiteSpace())
                    _columnNames = string.Join(", ", _repository.TableMetadata.SelectColumns
                        .Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}]", c.Name))
                        .ToArray());

                orderByColumns = _orderByColumns.IsNullOrEmpty()
                    ? string.Empty
                    : string.Join(", ", _orderByColumns);
            }
            else
            {
                if (_columnNames.IsNullOrWhiteSpace())
                {
                    _columnNames = string.Join(",\n", _repository.TableMetadata.SelectColumns
                        .Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}].[{1}] AS [{0}.{1}]", _repository.TableMetadata.Name, c.Name))
                        .ToArray());

                    foreach (var j in _joins)
                    {
                        if (!_columnNames.IsNullOrWhiteSpace())
                            _columnNames += ",\n";

                        _columnNames += string.Join(",\n", j.Value.SelectColumns
                            .Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}].[{1}] AS [{0}.{1}]", j.Key, c.Name))
                            .ToArray());
                    }
                }

                orderByColumns = _orderByColumns.IsNullOrEmpty()
                    ? string.Empty
                    : string.Join(", ", _orderByColumns.Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}].{1}", _repository.TableMetadata.Name, c.Trim())));

                foreach (var j in _joins)
                {
                    var onClause = string.Empty;
                    foreach (var c in j.Value.KeyColumns)
                    {
                        if (!onClause.IsNullOrWhiteSpace())
                            onClause += " AND ";

                        //This assumes convention where FK column names are the same as PK column names.
                        //TODO: Implement ForeignKeyAttribute and use its column names to make the "ON" clause for more complex scenarios.
                        onClause += string.Format(CultureInfo.InvariantCulture, "([{0}].[{2}] = [{1}].[{2}])", j.Key, _repository.TableMetadata.Name, c.Name);
                    }

                    joins.AppendLine(string.Format(CultureInfo.InvariantCulture, "LEFT OUTER JOIN [{0}] AS [{1}] ON {2}", j.Value.Name, j.Key, onClause));
                }
            }

            var sb = new StringBuilder();

            if (_skip == 0)
            {
                sb.Append("SELECT ");

                if (_take > 0)
                    sb.AppendFormat("TOP {0}\n", _take);

                sb.AppendLine(_columnNames);
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "FROM [{0}]", _repository.TableMetadata.Name));

                sb.Append(joins.ToString());

                if (!_where.IsNullOrWhiteSpace())
                    sb.AppendLine("WHERE " + _where);

                if (!orderByColumns.IsNullOrWhiteSpace())
                    sb.AppendLine("ORDER BY " + orderByColumns);
            }
            else
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS [RowNumber], {1} FROM [{2}]{3}{4}) AS [T] WHERE [RowNumber] > {5}",
                    orderByColumns.IsNullOrWhiteSpace() ? "(SELECT NULL)" : orderByColumns,
                    _columnNames,
                    _repository.TableMetadata.Name,
                    joins,
                    _where.IsNullOrWhiteSpace() ? string.Empty : " WHERE " + _where,
                    _skip);

                if (_take >= 0)
                    sb.AppendFormat(" AND [RowNumber] <= {0}", _skip + _take);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return this.ParseQuery();
        }
    }
}

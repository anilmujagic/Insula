using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using Insula.Common;

namespace Insula.Data.Orm
{
    public class Repository<T> where T : class, new()
    {
        private readonly DatabaseContext _ctx;
        private readonly TableMetadata _tableMetadata;
        private readonly ColumnMetadata _identityColumn;
        private readonly List<ColumnMetadata> _keyColumns;
        private readonly List<ColumnMetadata> _insertColumns;
        private readonly List<ColumnMetadata> _updateColumns;
        private readonly string _insertSql;
        private readonly string _updateSql;
        private readonly string _deleteSql;
        private readonly string _deleteByKeySql;
        private readonly string _keyQueryWhereClause;

        internal DatabaseContext DatabaseContext { get { return _ctx; } }
        internal TableMetadata TableMetadata { get { return _tableMetadata; } }
        internal Materializer<T> Materializer { get; private set; }

        public Repository(DatabaseContext context)
        {
            _ctx = context;

            _tableMetadata = new TableMetadata(typeof(T));

            _identityColumn = _tableMetadata.GetIdentityColumn();
            _keyColumns = _tableMetadata.GetKeyColumns().ToList();
            _insertColumns = _tableMetadata.GetInsertColumns().ToList();
            _updateColumns = _tableMetadata.GetUpdateColumns().ToList();

            _insertSql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})",
                _tableMetadata.Name,
                string.Join(", ", _insertColumns.Select(c => string.Format("[{0}]", c.Name))),
                string.Join(", ", _insertColumns.Select(c => string.Format("@{0}", c.Name))));

            if (_identityColumn != null)
                _insertSql += "; SELECT SCOPE_IDENTITY();";

            string keyWhereClause = string.Join(" AND ", _keyColumns.Select(c => string.Format("[{0}] = @{0}", c.Name)));

            _updateSql = string.Format("UPDATE [{0}] SET {1} WHERE {2}",
                _tableMetadata.Name,
                string.Join(", ", _updateColumns.Select(c => string.Format("[{0}] = @{0}", c.Name))),
                keyWhereClause);

            _deleteSql = string.Format("DELETE FROM [{0}] WHERE {1}",
                _tableMetadata.Name,
                keyWhereClause);

            int index = 0;
            _keyQueryWhereClause = string.Join(" AND ", _keyColumns.Select(c => string.Format("[{0}] = @{1}", c.Name, index++)));

            _deleteByKeySql = string.Format("DELETE FROM [{0}] WHERE {1}",
                _tableMetadata.Name,
                _keyQueryWhereClause);

            this.Materializer = new Materializer<T>(_tableMetadata);
        }

        public void Insert(T entity)
        {
            var parameters = new List<SqlParameter>();
            foreach (var c in _insertColumns)
            {
                var value = c.PropertyInfo.GetValue(entity, null);
                parameters.Add(new SqlParameter("@" + c.Name, value ?? DBNull.Value));
            }

            using (var command = _ctx.CreateCommand())
            {
                command.CommandText = _insertSql;
                command.Parameters.AddRange(parameters.ToArray());

                object newID;

                _ctx.OpenConnection();
                try
                {
                    newID = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    _ctx.OnException(ex, command.CommandText);
                    throw;
                }
                finally
                {
                    _ctx.CloseConnection();
                }

                if (_identityColumn != null)
                {
                    _identityColumn.PropertyInfo.SetValue(entity, Convert.ToInt32(newID), null);
                }
            }
        }

        public void Update(T entity)
        {
            if (_keyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for UPDATE statement to be valid.");

            var parameters = new List<SqlParameter>();
            foreach (var c in _updateColumns)
            {
                var value = c.PropertyInfo.GetValue(entity, null);
                parameters.Add(new SqlParameter("@" + c.Name, value ?? DBNull.Value));
            }
            foreach (var c in _keyColumns)
            {
                var value = c.PropertyInfo.GetValue(entity, null);
                parameters.Add(new SqlParameter("@" + c.Name, value ?? DBNull.Value));
            }

            using (var command = _ctx.CreateCommand())
            {
                command.CommandText = _updateSql;
                command.Parameters.AddRange(parameters.ToArray());

                _ctx.OpenConnection();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _ctx.OnException(ex, command.CommandText);
                    throw;
                }
                finally
                {
                    _ctx.CloseConnection();
                }
            }
        }

        public void Delete(T entity)
        {
            if (_keyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for DELETE statement to be valid.");

            var parameters = new List<SqlParameter>();
            foreach (var c in _keyColumns)
            {
                var value = c.PropertyInfo.GetValue(entity, null);
                parameters.Add(new SqlParameter("@" + c.Name, value ?? DBNull.Value));
            }

            using (var command = _ctx.CreateCommand())
            {
                command.CommandText = _deleteSql;
                command.Parameters.AddRange(parameters.ToArray());

                _ctx.OpenConnection();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _ctx.OnException(ex, command.CommandText);
                    throw;
                }
                finally
                {
                    _ctx.CloseConnection();
                }
            }
        }

        public void DeleteByKey(params object[] keyValues)
        {
            if (_keyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for DELETE statement to be valid.");
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");
            if (keyValues.Length != _keyColumns.Count)
                throw new ArgumentOutOfRangeException("keyValues", keyValues.Length, "Number of passed key values must be equal to number of key columns.");

            var parameters = new List<SqlParameter>();
            int index = 0;
            foreach (var value in keyValues)
            {
                parameters.Add(new SqlParameter("@" + index.ToString(), value ?? DBNull.Value));
                index++;
            }

            using (var command = _ctx.CreateCommand())
            {
                command.CommandText = _deleteByKeySql;
                command.Parameters.AddRange(parameters.ToArray());

                _ctx.OpenConnection();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _ctx.OnException(ex, command.CommandText);
                    throw;
                }
                finally
                {
                    _ctx.CloseConnection();
                }
            }
        }

        public T GetByKey(params object[] keyValues)
        {
            if (_keyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for GetByKey to be valid.");
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");
            if (keyValues.Length != _keyColumns.Count)
                throw new ArgumentOutOfRangeException("keyValues", keyValues.Length, "Number of passed key values must be equal to number of key columns.");

            return this.Query()
                .Where(_keyQueryWhereClause, keyValues)
                .GetSingle();
        }

        public SqlQuery<T> Query()
        {
            return new SqlQuery<T>(this);
        }
    }
}

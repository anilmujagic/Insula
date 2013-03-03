using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using Insula.Common;
using System.Globalization;

namespace Insula.Data.Orm
{
    public class Repository<T> where T : class, new()
    {
        private readonly DatabaseContext _ctx;
        private readonly TableMetadata _tableMetadata;
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

            _insertSql = string.Format(CultureInfo.InvariantCulture, 
                "INSERT INTO [{0}] ({1}) VALUES ({2})",
                _tableMetadata.Name,
                string.Join(", ", _tableMetadata.InsertColumns.Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}]", c.Name))),
                string.Join(", ", _tableMetadata.InsertColumns.Select(c => string.Format(CultureInfo.InvariantCulture, "@{0}", c.Name))));

            if (_tableMetadata.IdentityColumn != null)
                _insertSql += "; SELECT SCOPE_IDENTITY();";

            string keyWhereClause = string.Join(" AND ", _tableMetadata.KeyColumns
                .Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}] = @{0}", c.Name)));

            _updateSql = string.Format(CultureInfo.InvariantCulture, 
                "UPDATE [{0}] SET {1} WHERE {2}",
                _tableMetadata.Name,
                string.Join(", ", _tableMetadata.UpdateColumns.Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}] = @{0}", c.Name))),
                keyWhereClause);

            _deleteSql = string.Format(CultureInfo.InvariantCulture, 
                "DELETE FROM [{0}] WHERE {1}",
                _tableMetadata.Name,
                keyWhereClause);

            int index = 0;
            _keyQueryWhereClause = string.Join(" AND ", _tableMetadata.KeyColumns
                .Select(c => string.Format(CultureInfo.InvariantCulture, "[{0}] = @{1}", c.Name, index++)));

            _deleteByKeySql = string.Format(CultureInfo.InvariantCulture, 
                "DELETE FROM [{0}] WHERE {1}",
                _tableMetadata.Name,
                _keyQueryWhereClause);

            this.Materializer = new Materializer<T>(_tableMetadata);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var parameters = new List<SqlParameter>();
            foreach (var c in _tableMetadata.InsertColumns)
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

                if (_tableMetadata.IdentityColumn != null)
                {
                    _tableMetadata.IdentityColumn.PropertyInfo.SetValue(entity, Convert.ToInt32(newID, CultureInfo.InvariantCulture), null);
                }
            }
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (_tableMetadata.KeyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for UPDATE statement to be valid.");

            var parameters = new List<SqlParameter>();
            foreach (var c in _tableMetadata.UpdateColumns)
            {
                var value = c.PropertyInfo.GetValue(entity, null);
                parameters.Add(new SqlParameter("@" + c.Name, value ?? DBNull.Value));
            }
            foreach (var c in _tableMetadata.KeyColumns)
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
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (_tableMetadata.KeyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for DELETE statement to be valid.");

            var parameters = new List<SqlParameter>();
            foreach (var c in _tableMetadata.KeyColumns)
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
            if (_tableMetadata.KeyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for DELETE statement to be valid.");
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");
            if (keyValues.Length != _tableMetadata.KeyColumns.Count())
                throw new ArgumentOutOfRangeException("keyValues", keyValues.Length, "Number of passed key values must be equal to number of key columns.");

            var parameters = new List<SqlParameter>();
            int index = 0;
            foreach (var value in keyValues)
            {
                parameters.Add(new SqlParameter("@" + index.ToString(CultureInfo.InvariantCulture), value ?? DBNull.Value));
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
            if (_tableMetadata.KeyColumns.IsNullOrEmpty())
                throw new SqlStatementException("At least one object property must have a [Key] attribute for SELECT statement by primary key to be valid.");
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");
            if (keyValues.Length != _tableMetadata.KeyColumns.Count())
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

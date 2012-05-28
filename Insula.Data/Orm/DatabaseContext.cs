using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using Insula.Common;
using System.Globalization;

namespace Insula.Data.Orm
{
    public class DatabaseContext : IDisposable
    {
        private readonly string _connectionString;
        private readonly bool _keepConnectionOpen;
        private readonly IsolationLevel _isolationLevel;
        private TransactionScope _transactionScope;
        private SqlConnection _connection;

        public DatabaseContext(string connectionString, bool keepConnectionOpen = false,
            TransactionIsolationLevel transactionIsolationLevel = TransactionIsolationLevel.ReadCommitted)
        {
            if (connectionString.IsNullOrWhiteSpace())
                throw new ArgumentException("Connection string must not be empty.", "connectionString");

            _connectionString = connectionString;
            _keepConnectionOpen = keepConnectionOpen;
            _isolationLevel = transactionIsolationLevel.ToSystemTransactionsIsolationLevel();

            this.InitializeConnection();
        }


        #region Transactions/Connections

        private void InitializeConnection()
        {
            if (_transactionScope == null)
                _transactionScope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = _isolationLevel });

            if (_connection == null)
                _connection = new SqlConnection(_connectionString);
        }

        internal void OpenConnection()
        {
            this.InitializeConnection();

            switch (_connection.State)
            {
                case System.Data.ConnectionState.Broken:
                    _connection.Close();
                    _connection.Open();
                    break;
                case System.Data.ConnectionState.Closed:
                    _connection.Open();
                    break;
            }
        }

        internal void CloseConnection()
        {
            if (_connection == null)
                throw new InvalidOperationException("Connection is not initialized.");

            if (!_keepConnectionOpen && _connection.State != System.Data.ConnectionState.Closed)
                _connection.Close();
        }

        public void Commit()
        {
            this.CloseConnection();
            _connection.Dispose();
            _connection = null;

            if (_transactionScope == null)
                throw new InvalidOperationException("Transaction is not initialized.");

            _transactionScope.Complete();
            _transactionScope.Dispose();
            _transactionScope = null;

            this.InitializeConnection();
        }

        #endregion


        #region Commands

        internal SqlCommand CreateCommand()
        {
            this.InitializeConnection();
            return _connection.CreateCommand();
        }

        internal SqlCommand CreateCommand(string sql, params object[] parameters)
        {
            var command = this.CreateCommand();

            command.CommandText = sql;

            if (!parameters.IsNullOrEmpty())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.AddWithValue("@" + i.ToString(CultureInfo.InvariantCulture), parameters[i]);
                }
            }

            return command;
        }

        public object ExecuteScalar(string sql, params object[] parameters)
        {
            object result = null;

            using (var command = this.CreateCommand(sql, parameters))
            {
                this.OpenConnection();
                try
                {
                    result = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    this.OnException(ex, command.CommandText);
                    throw;
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return result;
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            int result = 0;

            using (var command = this.CreateCommand(sql, parameters))
            {
                this.OpenConnection();
                try
                {
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.OnException(ex, command.CommandText);
                    throw;
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return result;
        }

        #endregion


        public virtual void OnException(Exception exception, string sql)
        {
            System.Diagnostics.Debug.WriteLine(exception.GetExceptionTreeAsOneMessage());
            System.Diagnostics.Debug.WriteLine(sql);
        }


        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
                if (_transactionScope != null)
                {
                    _transactionScope.Dispose();
                    _transactionScope = null;
                }
            }
        }

        #endregion
    }
}

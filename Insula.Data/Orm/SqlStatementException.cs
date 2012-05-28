using System;

namespace Insula.Data.Orm
{
    public class SqlStatementException : Exception
    {
        public SqlStatementException()
        {
        }

        public SqlStatementException(string message)
            : base(message)
        {
        }

        public SqlStatementException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

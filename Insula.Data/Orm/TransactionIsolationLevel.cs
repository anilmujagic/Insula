using System;

namespace Insula.Data.Orm
{
    //This enumeration exists to avoid need to explicitly reference System.Transaction assembly in projects that use this assembly.
    //It is used as a parameter in DatabaseContext's constructor.

    public enum TransactionIsolationLevel
    {
        Serializable,
        RepeatableRead,
        ReadCommitted,
        ReadUncommitted,
        Snapshot,
        Chaos,
        Unspecified
    }

    public static class TransactionIsolationLevelExtensions
    {
        public static System.Transactions.IsolationLevel ToSystemTransactionsIsolationLevel(this TransactionIsolationLevel transactionIsolationLevel)
        {
            switch (transactionIsolationLevel)
            {
                case TransactionIsolationLevel.Serializable:
                    return System.Transactions.IsolationLevel.Serializable;
                case TransactionIsolationLevel.RepeatableRead:
                    return System.Transactions.IsolationLevel.RepeatableRead;
                case TransactionIsolationLevel.ReadCommitted:
                    return System.Transactions.IsolationLevel.ReadCommitted;
                case TransactionIsolationLevel.ReadUncommitted:
                    return System.Transactions.IsolationLevel.ReadUncommitted;
                case TransactionIsolationLevel.Snapshot:
                    return System.Transactions.IsolationLevel.Snapshot;
                case TransactionIsolationLevel.Chaos:
                    return System.Transactions.IsolationLevel.Chaos;
                case TransactionIsolationLevel.Unspecified:
                    return System.Transactions.IsolationLevel.Unspecified;
                default:
                    throw new Exception("Invalid transaction isolation level.");
            }
        }
    }
}

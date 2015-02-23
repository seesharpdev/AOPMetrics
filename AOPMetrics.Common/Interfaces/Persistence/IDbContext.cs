using System;

namespace AOPMetrics.Core.Interfaces.Persistence
{
    public interface IDbContext
    {
        void CommitChanges();

        IDisposable BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
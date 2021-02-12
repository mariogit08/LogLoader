using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TReuters.LogLoader.Domain.Interfaces;

namespace TReuters.LogLoader.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbTransaction Transaction { get; private set; }


        public UnitOfWork(IDbConnection connection)
        {
            Transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
                Transaction.Connection?.Close();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Transaction?.Dispose();                
                Transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                Transaction.Rollback();
                Transaction.Connection?.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                Transaction?.Dispose();
                Transaction.Connection?.Dispose();
                Transaction = null;
            }
        }
    }
}


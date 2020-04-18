using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieSessions.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace MovieSessions.Data
{
    public class SessionDatabase : DbContext
    {
        IDbContextTransaction? _currentTransaction;

        public DbSet<Movie> Movies { get; set; }

        public SessionDatabase(DbContextOptions<SessionDatabase> options)
            : base(options)
        {
        }


        public void BeginTransaction()
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CloseTransaction()
        {
            CloseTransaction(exception: null);
        }

        public void CloseTransaction(Exception? exception)
        {
            if (_currentTransaction == null)
                throw new InvalidOperationException("A transaction cannot be closed, because no transaction has been started.");

            try
            {
                if (exception != null)
                {
                    _currentTransaction.Rollback();
                    return;
                }

                SaveChanges();

                _currentTransaction.Commit();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Exception thrown while attempting to close a transaction.");
                _currentTransaction.Rollback();
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}

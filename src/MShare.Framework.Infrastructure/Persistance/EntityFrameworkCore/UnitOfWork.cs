using System;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Domain;

namespace MShare.Framework.Infrastructure.Persistance.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        private bool IsDisposed = false;

        private readonly DbContext _context;

        public UnitOfWork(DbContext context) => _context = context;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                _context.Dispose();
            }

            IsDisposed = true;
        }
    }
}


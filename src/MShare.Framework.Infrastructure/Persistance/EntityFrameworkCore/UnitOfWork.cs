using System;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Domain;

namespace MShare.Framework.Infrastructure.Persistance.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        private bool IsDisposed = false;
        private readonly bool _isRoot;

        private readonly DbContext _context;

        public UnitOfWork(DbContext context, bool isRoot) => (_context, _isRoot) = (context, isRoot);

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

            if (disposing && _isRoot)
            {
                _context.Dispose();
            }

            IsDisposed = true;
        }
    }
}


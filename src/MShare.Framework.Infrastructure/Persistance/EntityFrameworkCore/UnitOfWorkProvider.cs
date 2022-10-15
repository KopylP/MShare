using System;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Domain;

namespace MShare.Framework.Infrastructure.Persistance.EntityFramework
{
	public class UnitOfWorkProvider : IUnitOfWorkProvider
	{
        private readonly DbContext _context;
        private bool _rootCreated = false;

        public UnitOfWorkProvider(DbContext context)
        {
            _context = context;
        }

        public IUnitOfWork Create()
        {
            var uow = new UnitOfWork(_context, isRoot: !_rootCreated);
            _rootCreated = true;

            return uow;
        }
    }
}


using System;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Domain;

namespace MShare.Framework.Infrastructure.Persistance.EntityFramework
{
	public class UnitOfWorkProvider : IUnitOfWorkProvider
	{
        private readonly DbContext _context;

        public UnitOfWorkProvider(DbContext context) => _context = context;

        public IUnitOfWork Create() => new UnitOfWork(_context);
    }
}


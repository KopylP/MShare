using System;
namespace MShare.Framework.Domain
{
	public interface IUnitOfWork : IDisposable
	{
		Task CommitAsync();
		Task RollbackAsync();
	}
}
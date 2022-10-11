using System;
namespace MShare.Framework.Domain
{
	public interface IUnitOfWorkProvider
	{
		IUnitOfWork Create();
	}
}


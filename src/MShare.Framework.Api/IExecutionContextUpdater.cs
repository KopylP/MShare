using System;
using MShare.Framework.Api;

namespace MShare.Framework.Api
{
	public interface IExecutionContextUpdater
	{
		void Update(IExecutionContext newContext);
	}
}


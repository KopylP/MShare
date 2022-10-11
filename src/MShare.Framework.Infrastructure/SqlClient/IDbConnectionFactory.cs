using System;
using System.Data;

namespace MShare.Framework.Infrastructure.SqlClient
{
	public interface IDbConnectionFactory
	{
		IDbConnection Create();
	}
}


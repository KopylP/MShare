using System;
using System.Data;
using Npgsql;

namespace MShare.Framework.Infrastructure.SqlClient
{
	public class PostgresDbConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connection;

		public PostgresDbConnectionFactory(string connection)
		{
			_connection = connection;
		}

        public IDbConnection Create()
        {
			return new NpgsqlConnection(_connection);
        }
    }
}


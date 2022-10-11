using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MShare.Framework.Application.SqlClient;

namespace MShare.Framework.Infrastructure.SqlClient
{
	public class SqlQueryExecutor : ISqlQueryExecutor, IDisposable
	{
        private readonly IDbConnection _connection;

        public SqlQueryExecutor(IDbConnectionFactory factory)
        {
            _connection = factory.Create();
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default)
        {
            return (await _connection.QueryAsync<T>(sql, param)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default)
        {
            return await _connection.QuerySingleAsync<T>(sql, param);
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}


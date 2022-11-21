using System;
using System.Data;

namespace MShare.Framework.Application.SqlClient
{
	public interface ISqlQueryExecutor : IDisposable
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default);
        Task<T> QuerySingleAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default);
    }

    public static class ISqlQueryExecutorExtentions
    {
        public static async Task<IReadOnlyList<T>> QueryAsync<T>(this ISqlQueryExecutor executor, SqlBuilderBase sqlBuilder, CancellationToken cancellationToken = default)
        {
            var query = sqlBuilder.Build();
            return await executor.QueryAsync<T>(query.sql, query.parameters, cancellationToken);
        }

        public static async Task<T> QueryFirstOrDefaultAsync<T>(this ISqlQueryExecutor executor, SqlBuilderBase sqlBuilder, CancellationToken cancellationToken = default)
        {
            var query = sqlBuilder.Build();
            return await executor.QueryFirstOrDefaultAsync<T>(query.sql, query.parameters, cancellationToken);
        }

        public static async Task<T> QuerySingleAsync<T>(this ISqlQueryExecutor executor, SqlBuilderBase sqlBuilder, CancellationToken cancellationToken = default)
        {
            var query = sqlBuilder.Build();
            return await executor.QuerySingleAsync<T>(query.sql, query.parameters, cancellationToken);
        }
    }
}
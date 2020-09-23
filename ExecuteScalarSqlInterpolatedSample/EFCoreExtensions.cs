using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExecuteScalarInterpolatedSample
{
    public static class EFCoreExtensions
    {
#pragma warning disable EF1001 // Internal EF Core API usage.
        public static async Task<T> ExecuteScalarSqlInterpolatedAsync<T>(this DatabaseFacade database, FormattableString sql, CancellationToken cancellationToken = default)
        {
            // https://github.com/dotnet/efcore/issues/11624

            var databaseFacade = database as IDatabaseFacadeDependenciesAccessor;
            var facadeDependencies = databaseFacade.Dependencies as IRelationalDatabaseFacadeDependencies;
            var concurentDetector = facadeDependencies.ConcurrencyDetector;
            var logger = facadeDependencies.CommandLogger;

            using (concurentDetector.EnterCriticalSection())
            {
                var rawSqlCommand = facadeDependencies.RawSqlCommandBuilder
                    .Build(sql.Format, sql.GetArguments());

                return (T)await rawSqlCommand.RelationalCommand
                    .ExecuteScalarAsync(
                        new RelationalCommandParameterObject(
                            facadeDependencies.RelationalConnection,
                            rawSqlCommand.ParameterValues,
                            null,
                            databaseFacade.Context,
                            logger),
                        cancellationToken);
            }
        }
#pragma warning restore EF1001 // Internal EF Core API usage.    
    }
}

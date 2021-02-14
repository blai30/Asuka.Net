using System.Data;
using Asuka.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Asuka.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITagRepository _tags;

        private readonly ILogger<UnitOfWork> _logger;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        /// <summary>
        /// Tags repository.
        /// </summary>
        public ITagRepository Tags => _tags ??= new TagRepository(_transaction);

        public UnitOfWork(IServiceScopeFactory scopeFactory, ILogger<UnitOfWork> logger)
        {
            _logger = logger;

            // Get connection string.
            using var scope = scopeFactory.CreateScope();
            string connectionString = scope.ServiceProvider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("Docker");

            // New connection whenever unit of work is created.
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Save changes or rollback on failure.
        /// Dispose and restart transaction and reset repositories at the end.
        /// </summary>
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            _disposed = true;
            _logger.LogTrace("Unit of work disposed.");
        }

        private void ResetRepositories()
        {
            _tags = null;
        }

        ~UnitOfWork()
        {
            if (_disposed) return;
            _disposed = true;
        }
    }
}

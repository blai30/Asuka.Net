using System.Data;
using Asuka.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace Asuka.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITagRepository _tags;

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            string connectionString = scope.ServiceProvider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("Docker");

            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ITagRepository Tags
        {
            get
            {
                return _tags ??= new TagRepository(_transaction);
            }
        }

        public void Complete()
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

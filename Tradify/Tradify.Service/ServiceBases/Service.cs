using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Service.ServiceBases
{
    public class Service<T> : IService<T> where T : class

    {
        private readonly IGenericRepository<T> _repository;

        public Service(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<T> AddAsync(T entity)
        {
            try
            {
                return _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task AddRangeAsync(ICollection<T> entities)
        {
            try
            {
                return _repository.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            try
            {
                return _repository.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {

            try
            {
                return _repository.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Commit(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public Task CommitAsync(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                return dbContextTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                await _repository.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteRangeAsync(ICollection<T> entities)
        {
            try
            {
                await _repository.DeleteRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<T> GetByIdAsync(int id)
        {
            try
            {
                return _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<T> GetTableAsTracking()
        {
            try
            {
                return _repository.GetTableAsTracking();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<T> GetTableNoTracking()
        {
            try
            {
                return _repository.GetTableNoTracking();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RollBack(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                _repository.RollBack(dbContextTransaction);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RollBackAsync(IDbContextTransaction dbContextTransaction)
        {

            try
            {
                await _repository.RollBackAsync(dbContextTransaction);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task SaveChangesAsync()
        {
            try
            {
                return _repository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task UpdateAsync(T entity)
        {
            try
            {
                return _repository.UpdateAsync(entity);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task UpdateRangeAsync(ICollection<T> entities)
        {
            try
            {
                return _repository.UpdateRangeAsync(entities);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

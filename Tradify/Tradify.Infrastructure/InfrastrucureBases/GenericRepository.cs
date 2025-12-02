using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Infrastructure.InfrastrucureBases
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }
    }


}

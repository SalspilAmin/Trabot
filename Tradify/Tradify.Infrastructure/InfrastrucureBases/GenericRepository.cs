using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tradify.Infrastructure.Context;

namespace Tradify.Infrastructure.InfrastrucureBases
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        #region Properties
        private readonly ApplicationDbContext applicationDbContext;


        #endregion
        #region Constructor
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        #endregion
        public async Task<T> AddAsync(T entity)
        {
            
             await applicationDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await applicationDbContext.Set<T>().AddRangeAsync(entities);

        }

        public IDbContextTransaction BeginTransaction()
        {
           return applicationDbContext.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await applicationDbContext.Database.BeginTransactionAsync();
        }

      

        public void Commit(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Commit();
        }

        

        public async Task CommitAsync(IDbContextTransaction dbContextTransaction)
        {
           await dbContextTransaction.CommitAsync();
        }

        public async Task DeleteAsync(T entity)
        {
             applicationDbContext.Set<T>().Remove(entity);
            await applicationDbContext.SaveChangesAsync();

        }

        public async Task DeleteRangeAsync(ICollection<T> entities)
        {
            applicationDbContext.Set<T>().RemoveRange(entities);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await applicationDbContext.Set<T>().FindAsync(id);
        }

        public  IQueryable<T> GetTableAsTracking()
        {
            return applicationDbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return  applicationDbContext.Set<T>().AsNoTracking().AsQueryable();
          

        }

      

        public void RollBack(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Rollback();
        }

       
        public async Task RollBackAsync(IDbContextTransaction dbContextTransaction)
        {
            await dbContextTransaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
              applicationDbContext.Set<T>().Update(entity);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(ICollection<T> entities)
        {
            applicationDbContext.Set<T>().UpdateRange(entities);
            await applicationDbContext.SaveChangesAsync();
        }
    }


}

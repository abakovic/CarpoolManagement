using CarpoolManagement.Data.CarpoolManagementContext;
using CarpoolManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarpoolManagement.Core.Services
{
    public interface IService<TEntity> where TEntity : class, IEntity
    {
        TEntity GetEntity();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateAllAsync(List<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteAllAsync(List<TEntity> entities);
        Task<TEntity> GetByIdAsync(long id);
        IQueryable<TEntity> GetAll();
    }

    public class Service<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        private readonly CarpoolContext dbContext;
        public Service(CarpoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TEntity GetEntity()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAllAsync(List<TEntity> entities)
        {
            dbContext.UpdateRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(List<TEntity> entities)
        {
            dbContext.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await dbContext.Set<TEntity>()
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsNoTracking();
        }
    }
}

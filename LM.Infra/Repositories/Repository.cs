using LM.Domain.Entities;
using LM.Domain.Repositories;
using LM.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LM.Infra.Repositories
{
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : Entity
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public Repository(TDbContext context) => Context = context;

        public virtual async Task<Maybe<TEntity>> FindAsync(Guid code) => await DbSet.SingleOrDefaultAsync(x => x.Code == code);

        public Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRangeAsync(ICollection<TEntity> entityCollection)
        {
            foreach (var entity in entityCollection)
                UpdateAsync(entity);
            return Task.CompletedTask;
        }

        public async virtual Task AddRangeAsync(IEnumerable<TEntity> entityCollection)
            => await DbSet.AddRangeAsync(entityCollection);

        public virtual Task RemoveRangeAsync(ICollection<TEntity> entityCollection)
        {
            foreach (var entity in entityCollection)
                RemoveAsync(entity);
            return Task.CompletedTask;
        }

        public virtual Task<List<TEntity>> GetAllAsync() => DbSet.ToListAsync();

        public async virtual Task<List<TEntity>> FindAsync(List<Guid> codes)
            => await DbSet.Where(x => codes.Contains(x.Code)).ToListAsync();
    }
}

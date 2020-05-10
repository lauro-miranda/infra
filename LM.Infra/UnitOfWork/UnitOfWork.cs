using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using LM.Domain.UnitOfWork;

namespace LM.Infra.UnitOfWork
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        TDbContext Context { get; }

        public UnitOfWork(TDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CommitAsync() => await Context.SaveChangesAsync() > 0;
    }
}
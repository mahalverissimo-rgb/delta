using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Repositories
{
    /// <inheritdoc cref="IRepositoryBase{T}" />
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntityBase
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<List<T>> ListAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = DbSet;
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = DbSet;
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);

        public void Update(T entity) => DbSet.Update(entity);

        public void Remove(T entity) => DbSet.Remove(entity);

        public async Task<bool> ExistsAsync(int id) => await DbSet.AnyAsync(e => e.Id == id);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await DbSet.AnyAsync(predicate);

        public IQueryable<T> Query() => DbSet.AsQueryable();

        public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}

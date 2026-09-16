using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Repositories
{
    /// <summary>
    /// Repositório genérico de acesso a dados. Isola o Entity Framework das camadas
    /// superiores (serviços/controllers), tornando a regra de negócio testável.
    /// </summary>
    public interface IRepositoryBase<T> where T : class, IEntityBase
    {
        Task<List<T>> ListAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query();
        Task<int> SaveChangesAsync();
    }
}

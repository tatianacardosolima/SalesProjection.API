using SalesProjection.Domain.Abstractions;
using System.Linq.Expressions;

namespace SalesProjection.Application.Abstraction
{
    public interface IDefaultRepository<T> where T : IEntity
    {
        Task InsertAsync(T entity);
        Task<T>? GetByIdAsync(string id);
        Task<IEnumerable<T>?> GetAllAsync();        
        Task UpdateAsync(T entity, string id);
        Task DeleteAsync(Guid id);
    }
}

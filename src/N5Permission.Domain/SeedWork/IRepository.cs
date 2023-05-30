using System.Linq.Expressions;

namespace N5Permission.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<List<T>> GetAll();  
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity, bool activarDeteccion = false);
        Task Delete(object id);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate = null); 
    }
}

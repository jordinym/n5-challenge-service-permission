 
using Microsoft.EntityFrameworkCore;
using N5Permission.Domain.SeedWork;
using System.Linq.Expressions; 

namespace N5Permission.Infrastructure.Persistance.Repositories.Base
{

    public class BaseRepository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private DbSet<T> table = null;
        private readonly N5PermissionContext n5PermissionContext; 

        public BaseRepository(N5PermissionContext n5PermissionContext)
        {
            this.n5PermissionContext = n5PermissionContext ?? throw new ArgumentNullException(nameof(N5PermissionContext));  

            table = n5PermissionContext.Set<T>();
        }

        #region Public Methods

        public async Task<List<T>> GetAll() => await table.ToListAsync(); 

        public async Task<T> GetById(Expression<Func<T, bool>> predicate) => await table.FindAsync(predicate);

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => table.FirstOrDefaultAsync(predicate);

        public void Insert(T entity) =>   table.Add(entity);

        public void Update(T entity, bool activarDeteccion = false)
        {
            table.Attach(entity);
            n5PermissionContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }
         
        public Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) => table.Where(predicate).ToListAsync();

        public IQueryable<T> GetWhere(Expression<Func<T, bool>>? predicate = null)=> predicate != null ? table.Where(predicate) : table.AsQueryable();
         
        #endregion
  
    }
}

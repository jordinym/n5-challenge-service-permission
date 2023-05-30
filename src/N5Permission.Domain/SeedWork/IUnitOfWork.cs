namespace N5Permission.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    { 
        void Dispose(); 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Dispose(bool disposing);
        T Repository<T>() where T : class; 
        /// <summary>
        /// Inicia una transacción. Usar cuando se desee usar multiples Context.Savechanges.
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();
        /// <summary>
        /// Confirma los cambios de una transacción. Donde se han realizado multiples Context.SaveChanges. ejemplo:
        ///  public method1 () { Context.SaveChanges(); }
        ///  public method2 () { Context.SaveChanges(); }
        ///  public methodMain() {  method1() ; method2(); _unit.CommitAsync(); }
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Revierte los cambios de uno o  multipes Context.SaveChanges().
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}

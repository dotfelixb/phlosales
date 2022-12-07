namespace PhloSales.Data;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class, IEntity;

    Task<int> Commit(CancellationToken cancellationToken);

    Task Rollback();
}
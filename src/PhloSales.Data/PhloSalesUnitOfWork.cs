using PhloSales.Data.EntityContext;
using PhloSales.Data.Repositories;
using System.Collections;

namespace PhloSales.Data;

public class PhloSalesUnitOfWork : IUnitOfWork
{
    private readonly PhloSalesDbContext _context;
    private readonly Hashtable _repositories;
    private bool disposed;

    public PhloSalesUnitOfWork(PhloSalesDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public IRepository<T> Repository<T>() where T : class, IEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(PhloSalesRepository<>);

            var instance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

            _repositories.Add(type, instance);
        }

        return (IRepository<T>)_repositories[type];
    }

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Rollback()
    {
        var entries = _context.ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            await entry.ReloadAsync();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _context.Dispose();
        }

        disposed = true;
    }
}
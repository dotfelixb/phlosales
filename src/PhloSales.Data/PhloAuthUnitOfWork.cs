using PhloSales.Data.EntityContext;
using PhloSales.Data.Repositories;
using System.Collections;

namespace PhloSales.Data;

public class PhloAuthUnitOfWork : IUnitOfWork
{
    private readonly PhloAuthDbContext _context;
    private readonly Hashtable _repositories;
    private bool disposed;

    public PhloAuthUnitOfWork(PhloAuthDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public IRepository<T> Repository<T>() where T : class, IEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(PhloAuthRepository<>);

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
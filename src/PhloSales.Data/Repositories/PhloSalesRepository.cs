using Microsoft.EntityFrameworkCore;
using PhloSales.Data.EntityContext;

namespace PhloSales.Data.Repositories;

public class PhloSalesRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly PhloSalesDbContext _context;

    public PhloSalesRepository(PhloSalesDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

    public async Task AddAsync(IEnumerable<T> entities) => await _context.Set<T>().AddRangeAsync(entities);

    public async Task<T> GetAsync(int id) => await _context.Set<T>().FindAsync(id);

    public async Task<List<T>> GetAsync() => await _context.Set<T>().ToListAsync();
}
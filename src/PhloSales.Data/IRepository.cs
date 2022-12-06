namespace PhloSales.Data;

public interface IRepository<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }

    Task<T> GetAsync(int id);

    Task<List<T>> GetAsync();

    Task AddAsync(T entity);

    Task AddAsync(IEnumerable<T> entities);
}

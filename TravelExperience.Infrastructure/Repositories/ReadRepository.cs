using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelExperience.Domain.Core;
using TravelExperience.Infrastructure.Data;

namespace TravelExperience.Infrastructure.Repositories;
public class ReadRepository<T> : IReadRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly TravelExperienceDbContext _context;

    public ReadRepository(TravelExperienceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public DbSet<T> Table => _context.Set<T>();

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
}
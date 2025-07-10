using Microsoft.EntityFrameworkCore;
using TravelExperience.Domain.Core;
using TravelExperience.Infrastructure.Data;

namespace TravelExperience.Infrastructure.Repositories;
public class WriteRepository<T> : IWriteRepository<T> where T : class
{
    private readonly TravelExperienceDbContext _context;
    private readonly DbSet<T> _dbSet;

    public WriteRepository(TravelExperienceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}

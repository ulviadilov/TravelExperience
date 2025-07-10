using System.Linq.Expressions;

namespace TravelExperience.Domain.Core;
public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    IQueryable<T> GetAll(bool tracking = true);
}

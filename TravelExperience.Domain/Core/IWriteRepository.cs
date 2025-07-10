namespace TravelExperience.Domain.Core;
public interface IWriteRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
}
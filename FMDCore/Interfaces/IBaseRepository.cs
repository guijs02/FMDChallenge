namespace FMDCore.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
    }
}

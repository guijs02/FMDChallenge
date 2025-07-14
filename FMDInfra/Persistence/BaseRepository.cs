using FMDCore.Interfaces;
using FMDInfra.Data;
using Microsoft.EntityFrameworkCore;

namespace FMDInfra.Persistence
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public abstract Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

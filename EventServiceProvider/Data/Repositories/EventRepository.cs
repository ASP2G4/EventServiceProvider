using EventServiceProvider.Data.Contexts;
using EventServiceProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventServiceProvider.Data.Repositories;

public interface IEventRepository
{
    Task<bool> AddAsync(EventEntity entity);
    Task<bool> DeleteAsync(Expression<Func<EventEntity, bool>> expression);
    Task<IEnumerable<EventEntity>> GetAllAsync();
    Task<EventEntity?> GetAsync(Expression<Func<EventEntity, bool>> expression);
    Task<bool> UpdateAsync(EventEntity entity);
}

public class EventRepository : IEventRepository
{
    protected readonly DataContext _context;
    protected readonly DbSet<EventEntity> _table;

    public EventRepository(DataContext context)
    {
        _context = context;
        _table = context.Set<EventEntity>();
    }

    /* Create */
    public async Task<bool> AddAsync(EventEntity entity)
    {
        if (entity == null)
            return false;

        await _table.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    /* Read */
    public virtual async Task<IEnumerable<EventEntity>> GetAllAsync()
    {
        var entities = await _table.ToListAsync();
        return entities;
    }

    public virtual async Task<EventEntity?> GetAsync(Expression<Func<EventEntity, bool>> expression)
    {
        var entity = await _table.FirstOrDefaultAsync(expression);
        return entity;
    }


    /* Update */
    public virtual async Task<bool> UpdateAsync(EventEntity entity)
    {
        if (entity == null)
            return false;

        _table.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }


    /* Delete */
    public virtual async Task<bool> DeleteAsync(Expression<Func<EventEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        var entity = await _table.FirstOrDefaultAsync(expression);
        if (entity == null)
            return false;

        _table.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

}

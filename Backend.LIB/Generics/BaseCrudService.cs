using System.Linq.Expressions;

namespace Backend.LIB.Generics;

public class BaseCrudService<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    private readonly TContext _ctx;
    public BaseCrudService(TContext context)
    {
        _ctx = context;
    }
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _ctx.Set<TEntity>().AddAsync(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _ctx.Set<TEntity>().Update(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _ctx.Set<TEntity>().Remove(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _ctx.Set<TEntity>()
            .FirstOrDefaultAsync(predicate);
    }

    public virtual TEntity Create(TEntity entity)
    {
        _ctx.Set<TEntity>().Add(entity);
        _ctx.SaveChanges();
        return entity;
    }
    public virtual TEntity Update(TEntity entity)
    {
        _ctx.Set<TEntity>().Update(entity);
        _ctx.SaveChanges();
        return entity;
    }
    public virtual void Delete(TEntity entity)
    {
        _ctx.Set<TEntity>().Remove(entity);
        _ctx.SaveChangesAsync();
    }
    public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
        return _ctx.Set<TEntity>().FirstOrDefault(predicate);
    }
    public virtual IEnumerable<TEntity> GetAll()
    {
        return _ctx.Set<TEntity>();
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        return _ctx.Set<TEntity>()
            .Where(predicate);
    }
}

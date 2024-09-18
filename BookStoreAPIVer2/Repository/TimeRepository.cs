using System.Linq.Expressions;
using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPIVer2.Repository;

public class TimeRepository : ITimeRepository
{
    private readonly ApplicationDbContext _db;
    internal DbSet<TimeKeeping> _dbSet;

    public TimeRepository(ApplicationDbContext db)
    {
        _db = db;
        this._dbSet = _db.Set<TimeKeeping>();
    }

    public async Task<List<TimeKeeping>> GetAllAsync()
    {
        var timeKeepingList = await _dbSet.ToListAsync();
        return timeKeepingList;
    }

    public async Task<TimeKeeping> AddAsync(TimeKeeping entity)
    {
        var newTimeKeeping = await _dbSet.AddAsync(entity);
        
        await _db.SaveChangesAsync();
        
        return newTimeKeeping.Entity;
    }

    public async Task UpdateAsync(TimeKeeping entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        
        await _db.SaveChangesAsync();
    }

    public async Task<TimeKeeping> GetAsync(Expression<Func<TimeKeeping, bool>> filter = null)
    {
        IQueryable<TimeKeeping> query = _dbSet.OrderByDescending(time => time.StartTime);
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        return await query.FirstOrDefaultAsync();
    }
}
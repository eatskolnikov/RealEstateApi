
using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Data;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    DbSet<T> dbSet;
    ApplicationDbContext applicationDbContext;
    public BaseRepository(ApplicationDbContext dbContext){
        dbSet = dbContext.Set<T>();
        applicationDbContext = dbContext;
    }
    public async Task Delete(T entity)
    {
        try{
            dbSet.Remove(entity);
            await applicationDbContext.SaveChangesAsync();
        }catch(Exception ex){
            throw ex;
        }
    }

    public IQueryable<T> GetAll()
    {
        return dbSet.Where(x=> true).AsQueryable<T>();
    }

    public async Task<T> GetById(string id)
    {
        return await dbSet.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task Insert(T entity)
    {
        try{
            await dbSet.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();
        }catch(Exception ex){
            throw ex;
        }
    }

    public async Task Update(T entity)
    {
        try{
            dbSet.Update(entity);
            await applicationDbContext.SaveChangesAsync();
        }catch(Exception ex){
            throw ex;
        }
    }
}

public interface IBaseRepository<T> where T:BaseEntity{
    IQueryable<T> GetAll();
    Task<T> GetById(string Id);
    Task Insert(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
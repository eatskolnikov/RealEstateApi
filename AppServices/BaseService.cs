public abstract class BaseService<T> : IBaseService<T> where T:BaseEntity
{
    protected IBaseRepository<T> _mainRepository;

    public BaseService(IBaseRepository<T> mainRepository){
        _mainRepository = mainRepository;
    }
    
    public IQueryable<T> GetAll()
    {
        return _mainRepository.GetAll();
    }

    public async Task<T> GetById(string id)
    {
        
        return await _mainRepository.GetById(id);
    }

    public async Task<TaskResult> Delete(T entity)
    {
        var taskResult = ValidateDelete(entity); 
        if(taskResult.Success)
        {
            try{
                await _mainRepository.Delete(entity);
                return new TaskResult(true, "Item deleted successfully");
            }catch(Exception ex)
            {
                return new TaskResult(false, ex.Message);
            }
        }
        return taskResult;
    }

    public async Task<TaskResult<T>> Insert(T entity)
    {
        var taskResult = ValidateInsert(entity); 
        if(taskResult.Success)
        {
            try{
                entity.Id = Guid.NewGuid().ToString();
                await _mainRepository.Insert(entity);
                return new TaskResult<T>(true, "Item inserted successfully", entity);
            }catch(Exception ex)
            {
                return new TaskResult<T>(false, ex.Message, null);
            }
        }
        return new TaskResult<T>(taskResult.Success, taskResult.Message, null);;
    }

    public async Task<TaskResult> Update(T entity)
    {
        var taskResult = ValidateUpdate(entity); 
        if(taskResult.Success)
        {
            try{
               await _mainRepository.Update(entity);
                return new TaskResult(true, "Item updated successfully");
            }catch(Exception ex)
            {
                return new TaskResult(false, ex.Message);
            }
        }
        return taskResult;
    }
    public abstract TaskResult ValidateInsert(T entity);
    public abstract TaskResult ValidateUpdate(T entity);
    public abstract TaskResult ValidateDelete(T entity);
}

public interface IBaseService<T> where T:BaseEntity{

    TaskResult ValidateInsert(T entity);
    TaskResult ValidateUpdate(T entity);
    TaskResult ValidateDelete(T entity);
    Task<TaskResult<T>> Insert(T entity);
    Task<TaskResult> Update(T entity);
    Task<TaskResult> Delete(T entity);

    IQueryable<T> GetAll();
    Task<T> GetById(string id);
    
}
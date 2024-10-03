
using RealEstateListingApi.Models;

public class ListingsService : BaseService<Listing>, IListingsService
{
    public ListingsService(IListingsRepository mainRepository) : base(mainRepository)
    {
    }

    public override TaskResult ValidateUpdate(Listing entity)
    {
        if(string.IsNullOrEmpty(entity.Title)){
            return new TaskResult(false, "Title cannot be empty");
        }
        return new TaskResult(true, string.Empty);
    }

    public override TaskResult ValidateDelete(Listing entity)
    {
        return new TaskResult(true, string.Empty);
    }

    public override TaskResult ValidateInsert(Listing entity)
    {
        return new TaskResult(true, string.Empty);
    }
}

public interface IListingsService : IBaseService<Listing>{

}
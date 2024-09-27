
using RealEstateListingApi.Models;

public class ListingsService : BaseService<Listing>, IListingsService
{
    public ListingsService(IListingsRepository mainRepository) : base(mainRepository)
    {
    }

    public override TaskResult ValiateUpdate(Listing entity)
    {
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
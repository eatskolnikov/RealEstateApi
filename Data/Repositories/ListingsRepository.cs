
using RealEstateListingApi.Data;
using RealEstateListingApi.Models;

class ListingsRepository : BaseRepository<Listing>, IListingsRepository
{
    public ListingsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

public interface IListingsRepository : IBaseRepository<Listing>{

}
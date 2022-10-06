namespace WasteSegregation.Domain.Interfaces;

public interface IRealEstateRepository
{
    Task<IEnumerable<RealEstate>> GetAllAsync(int pageNumber, int pageSize);
    Task<int> GetAllCountAsync();
    Task<RealEstate> GetByIdAsync(int id);
    Task<RealEstate> AddAsync(RealEstate realEstate);
    Task UpdateAsync(RealEstate realEstate);
    Task DeleteAsync(RealEstate realEstate);
}

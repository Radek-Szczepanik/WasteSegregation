namespace WasteSegregation.Domain.Interfaces;

public interface IRealEstateRepository
{
    Task<IEnumerable<RealEstate>> GetAllAsync();
    Task<RealEstate> GetByIdAsync(int id);
    Task<RealEstate> AddAsync(RealEstate realEstate);
    Task UpdateAsync(RealEstate realEstate);
    Task DeleteAsync(RealEstate realEstate);
}

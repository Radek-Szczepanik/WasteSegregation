namespace WasteSegregation.Domain.Interfaces;

public interface IRealEstateRepository
{
    Task<IEnumerable<RealEstate>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy);
    Task<int> GetAllCountAsync(string filterBy);
    Task<RealEstate> GetByIdAsync(int id);
    Task<RealEstate> AddAsync(RealEstate realEstate);
    Task UpdateAsync(RealEstate realEstate);
    Task DeleteAsync(RealEstate realEstate);
}

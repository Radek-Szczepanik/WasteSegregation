namespace WasteSegregation.Application.Interfaces;

public interface IRealEstateService
{
    Task<IEnumerable<RealEstateDto>> GetAllAsync();
    Task<RealEstateDto> GetByIdAsync(int id);
    Task<RealEstateDto> AddAsync(CreateRealEstateDto createRealEstate, string userId);
    Task UpdateAsync(UpdateRealEstateDto updateRealEstate, int id);
    Task DeleteAsync(int id);
}

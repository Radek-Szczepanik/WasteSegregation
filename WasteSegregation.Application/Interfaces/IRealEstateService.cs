namespace WasteSegregation.Application.Interfaces;

public interface IRealEstateService
{
    Task<IEnumerable<RealEstateDto>> GetAllAsync();
    Task<RealEstateDto> GetByIdAsync(int id);
    Task<RealEstateDto> AddAsync(CreateRealEstateDto createRealEstate);
    Task UpdateAsync(UpdateRealEstateDto updateRealEstate);
    Task DeleteAsync(int id);
}

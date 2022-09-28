namespace WasteSegregation.Application.Interfaces;

public interface IWasteBagsService
{
    Task<int> AddAsync(int realEstateId, CreateWasteBagsDto createWasteBagsDto, string UserId);
    Task<bool> RealEstateWasteBagsAsync(int realEstateId, string UserId);
}

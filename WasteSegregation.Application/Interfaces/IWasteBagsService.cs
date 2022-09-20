namespace WasteSegregation.Application.Interfaces;

public interface IWasteBagsService
{
    Task<int> AddAsync(int realEstateId, CreateWasteBagsDto createWasteBagsDto);
}

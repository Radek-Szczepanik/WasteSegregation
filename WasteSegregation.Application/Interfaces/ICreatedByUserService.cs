namespace WasteSegregation.Application.Interfaces;

public interface ICreatedByUserService
{
    Task<bool> IsUserCreatedAsync(int realEstateId, string userId);
}

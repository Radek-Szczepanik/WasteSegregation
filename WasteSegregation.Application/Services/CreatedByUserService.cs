namespace WasteSegregation.Application.Services;

public class CreatedByUserService : ICreatedByUserService
{
    private readonly IRealEstateRepository realEstateRepository;

    public CreatedByUserService(IRealEstateRepository realEstateRepository)
    {
        this.realEstateRepository = realEstateRepository;
    }

    public async Task<bool> IsUserCreatedAsync(int realEstateId, string userId)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(realEstateId);
        if (realEstate == null)
        {
            return false;
        }

        if (realEstate.UserId != userId)
        {
            return false;
        }

        return true;
    }
}

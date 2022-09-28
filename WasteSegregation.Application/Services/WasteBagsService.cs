namespace WasteSegregation.Application.Services;

public class WasteBagsService : IWasteBagsService
{
    private readonly IWasteBagsRepository wasteBagsRepository;
    private readonly IRealEstateRepository realEstateRepository;
    private readonly IMapper mapper;

    public WasteBagsService(IWasteBagsRepository wasteBagsRepository,
                            IRealEstateRepository realEstateRepository,
                            IMapper mapper)
    {
        this.wasteBagsRepository = wasteBagsRepository;
        this.realEstateRepository = realEstateRepository;
        this.mapper = mapper;
    }
    public async Task<int> AddAsync(int realEstateId, CreateWasteBagsDto createWasteBagsDto, string UserId)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(realEstateId);
        if (realEstate == null) throw new NotFoundException("Real estate not found");
        var wasteBags = mapper.Map<WasteBag>(createWasteBagsDto);
        wasteBags.UserId = UserId;
        wasteBags.RealEstateId = realEstateId;
        await wasteBagsRepository.AddAsync(wasteBags);
        return wasteBags.Id;
    }

    public async Task<bool> RealEstateWasteBagsAsync(int realEstateId, string UserId)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(realEstateId);
        if (realEstate == null)
        {
            return false;
        }

        if (realEstate.UserId != UserId)
        {
            return false;
        }

        return true;
    }
}

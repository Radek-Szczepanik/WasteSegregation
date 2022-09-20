namespace WasteSegregation.Application.Services;

public class WasteBagsService : IWasteBagsService
{
    private readonly IWasteBagsRepository wasteRepository;
    private readonly IRealEstateRepository realEstateRepository;
    private readonly IMapper mapper;

    public WasteBagsService(IWasteBagsRepository wasteRepository, IRealEstateRepository realEstateRepository, IMapper mapper)
    {
        this.wasteRepository = wasteRepository;
        this.realEstateRepository = realEstateRepository;
        this.mapper = mapper;
    }
    public async Task<int> AddAsync(int realEstateId, CreateWasteBagsDto createWasteBagsDto)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(realEstateId);
        if (realEstate == null) throw new NotFoundException("Real estate not found");
        var wasteBags = mapper.Map<WasteBag>(createWasteBagsDto);
        wasteBags.RealEstateId = realEstateId;
        await wasteRepository.AddAsync(wasteBags);
        return wasteBags.Id;
    }
}

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
    public async Task<int> AddAsync(int realEstateId, CreateWasteBagsDto createWasteBagsDto)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(realEstateId);
        if (realEstate == null) throw new NotFoundException("Real estate not found");
        var wasteBags = mapper.Map<WasteBag>(createWasteBagsDto);
        wasteBags.RealEstateId = realEstateId;
        await wasteBagsRepository.AddAsync(wasteBags);
        return wasteBags.Id;
    }
}

using System.Linq;

namespace WasteSegregation.Application.Services;

public class RealEstateService : IRealEstateService
{
    private readonly IRealEstateRepository realEstateRepository;
    private readonly IMapper mapper;

    public RealEstateService(IRealEstateRepository realEstateRepository, IMapper mapper)
    {
        this.realEstateRepository = realEstateRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<RealEstateDto>> GetAllAsync()
    {
        var realEstate = await realEstateRepository.GetAllAsync();
        if (realEstate == null) return null;
        return mapper.Map<List<RealEstateDto>>(realEstate);
    }

    public async Task<RealEstateDto> GetByIdAsync(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        if (realEstate == null) return null;
        return mapper.Map<RealEstateDto>(realEstate);
    }

    public async Task<RealEstateDto> AddAsync(CreateRealEstateDto createRealEstate)
    {
        var realEstate = mapper.Map<RealEstate>(createRealEstate);
        var result = await realEstateRepository.AddAsync(realEstate);
        return mapper.Map<RealEstateDto>(result);
    }

    public Task UpdateAsync(UpdateRealEstateDto updateRealEstate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}

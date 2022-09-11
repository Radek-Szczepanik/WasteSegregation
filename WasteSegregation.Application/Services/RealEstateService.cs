﻿namespace WasteSegregation.Application.Services;

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

    public async Task UpdateAsync(UpdateRealEstateDto updateRealEstate, int id)
    {
        var existingRealEstate = await realEstateRepository.GetByIdAsync(id);
        var realEstate = mapper.Map(updateRealEstate, existingRealEstate);
        await realEstateRepository.UpdateAsync(realEstate);
    }

    public async Task DeleteAsync(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        await realEstateRepository.DeleteAsync(realEstate);
    }
}

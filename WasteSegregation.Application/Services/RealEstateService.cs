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

    public async Task<IEnumerable<RealEstateDto>> GetAllAsync(int pageNumber, int pageSize, string sortField,
                                                              bool ascending, string filterBy)
    {
        var realEstate = await realEstateRepository.GetAllAsync(pageNumber, pageSize, sortField, ascending, filterBy);
        if (realEstate == null) throw new NotFoundException("Real estates not found");
        return mapper.Map<List<RealEstateDto>>(realEstate);
    }

    public async Task<RealEstateDto> GetByIdAsync(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        if (realEstate == null) throw new NotFoundException("Real estate not found");
        return mapper.Map<RealEstateDto>(realEstate);
    }

    public async Task<RealEstateDto> AddAsync(CreateRealEstateDto createRealEstate, string userId)
    {
        var realEstate = mapper.Map<RealEstate>(createRealEstate);
        realEstate.UserId = userId;
        var result = await realEstateRepository.AddAsync(realEstate);
        return mapper.Map<RealEstateDto>(result);
    }

    public async Task UpdateAsync(UpdateRealEstateDto updateRealEstate, int id)
    {
        var existingRealEstate = await realEstateRepository.GetByIdAsync(id);
        if (existingRealEstate == null) throw new NotFoundException("Real estate not found");
        var realEstate = mapper.Map(updateRealEstate, existingRealEstate);
        await realEstateRepository.UpdateAsync(realEstate);
    }

    public async Task DeleteAsync(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        if (realEstate == null) throw new NotFoundException("Real estate not found");
        await realEstateRepository.DeleteAsync(realEstate);
    }

    public async Task<int> GetAllRealEstatesCountAsync(string filterBy)
    {
        return await realEstateRepository.GetAllCountAsync(filterBy);
    }
}

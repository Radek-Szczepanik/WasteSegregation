namespace WasteSegregation.Application.Mappings;

public class MappingsProfile : Profile
{
    public static IMapper Initialize() => new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<RealEstate, RealEstateDto>();
        cfg.CreateMap<RealEstateWaste, RealEstateWasteDto>();
        cfg.CreateMap<CreateRealEstateDto, RealEstate>();
        cfg.CreateMap<UpdateRealEstateDto, RealEstate>();
    })
    .CreateMapper();
}

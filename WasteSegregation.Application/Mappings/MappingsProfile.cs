namespace WasteSegregation.Application.Mappings;

public class MappingsProfile : Profile
{
    public static IMapper Initialize() => new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<RealEstate, RealEstateDto>();
        cfg.CreateMap<RealEstateWaste, RealEstateWasteDto>();
        cfg.CreateMap<CreateRealEstateDto, RealEstate>();
        cfg.CreateMap<UpdateRealEstateDto, RealEstate>()
            .ForMember(x => x.Street, y => y.MapFrom(z => z.Street))
            .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.StreetNumber))
            .ForMember(x => x.PostCode, y => y.MapFrom(z => z.PostCode))
            .ForMember(x => x.City, y => y.MapFrom(z => z.City));
    })
    .CreateMapper();
}

namespace WasteSegregation.Application.Mappings;

public class RealEstateMappingProfile : Profile
{
	public RealEstateMappingProfile()
	{
		CreateMap<RealEstate, RealEstateDto>();

		CreateMap<RealEstateWaste, RealEstateWasteDto>();

		CreateMap<CreateRealEstateDto, RealEstate>();
	}
}

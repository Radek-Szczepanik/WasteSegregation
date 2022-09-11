namespace WasteSegregation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RealEstatesController : ControllerBase
{
	private readonly IRealEstateService realEstateService;

	public RealEstatesController(IRealEstateService realEstateService)
	{
		this.realEstateService = realEstateService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var realEstates = await realEstateService.GetAllAsync();
		return Ok(realEstates);
	}

	[HttpGet]
	[Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var realEstate = await realEstateService.GetByIdAsync(id);
        return Ok(realEstate);
    }

    [HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateRealEstateDto createRealEstateDto)
	{
		var newRealEstate = await realEstateService.AddAsync(createRealEstateDto);
		return Created($"api/realEstates/{newRealEstate.Id}", null);
	}
}

namespace WasteSegregation.WebAPI.Controllers;

[Authorize]
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
		var newRealEstate = await realEstateService.AddAsync(createRealEstateDto, User.FindFirstValue(ClaimTypes.NameIdentifier));
		return Created($"api/realEstates/{newRealEstate.Id}", null);
	}

    [HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> Update([FromBody] UpdateRealEstateDto updateRealEstateDto, [FromRoute] int id)
	{
		var userOwnsRealEstate = await realEstateService.UserOwnsRealEstateAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		if (!userOwnsRealEstate)
		{
			return BadRequest(new Response<bool>()
			{
				Succeeded = false,
				Message = "You did not create this real estate"
			});
		}

		await realEstateService.UpdateAsync(updateRealEstateDto, id);
		return NoContent();
	}

    [HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> Delete([FromRoute] int id)
	{
        var userOwnsRealEstate = await realEstateService.UserOwnsRealEstateAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (!userOwnsRealEstate)
        {
            return BadRequest(new Response<bool>()
            {
                Succeeded = false,
                Message = "You did not create this real estate"
            });
        }
        await realEstateService.DeleteAsync(id);
		return NoContent();
	}
}

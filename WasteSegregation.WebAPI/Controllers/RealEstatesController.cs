namespace WasteSegregation.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RealEstatesController : ControllerBase
{
	private readonly IRealEstateService realEstateService;
	private readonly ICreatedByUserService createdByUserService;

	public RealEstatesController(IRealEstateService realEstateService, ICreatedByUserService createdByUserService)
	{
		this.realEstateService = realEstateService;
		this.createdByUserService = createdByUserService;
	}

    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var realEstates = await realEstateService.GetAllAsync();
		return Ok(realEstates);
	}

    [Authorize(Roles = UserRoles.AdminOrUser)]
    [HttpGet]
	[Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var isUserCreated = await createdByUserService.IsUserCreatedAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		var isAdmin = User.FindFirstValue(ClaimTypes.Role).Contains(UserRoles.Admin);
        var realEstate = await realEstateService.GetByIdAsync(id);
        if (!isAdmin && !isUserCreated)
        {
            return BadRequest(new Response<bool>()
            {
                Succeeded = false,
                Message = "You did not create this real estate"
            });
        }
        
        return Ok(realEstate);
    }

    [Authorize(Roles = UserRoles.User)]
    [HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateRealEstateDto createRealEstateDto)
	{
        var newRealEstate = await realEstateService.AddAsync(createRealEstateDto, User.FindFirstValue(ClaimTypes.NameIdentifier));
		return Created($"api/realEstates/{newRealEstate.Id}", null);
	}

    [Authorize(Roles = UserRoles.User)]
    [HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> Update([FromBody] UpdateRealEstateDto updateRealEstateDto, [FromRoute] int id)
	{
		var isUserCreated = await createdByUserService.IsUserCreatedAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        await realEstateService.UpdateAsync(updateRealEstateDto, id);
        if (!isUserCreated)
		{
			return BadRequest(new Response<bool>()
			{
				Succeeded = false,
				Message = "You did not create this real estate"
			});
		}

		return NoContent();
	}

    [Authorize(Roles = UserRoles.User)]
    [HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> Delete([FromRoute] int id)
	{
        var isUserCreated = await createdByUserService.IsUserCreatedAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        await realEstateService.DeleteAsync(id);
        if (!isUserCreated)
        {
            return BadRequest(new Response<bool>()
            {
                Succeeded = false,
                Message = "You did not create this real estate"
            });
        }
        
		return NoContent();
	}
}

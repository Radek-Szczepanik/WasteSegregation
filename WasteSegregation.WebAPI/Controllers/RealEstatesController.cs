namespace WasteSegregation.WebAPI.Controllers;

[AllowAnonymous]
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
	public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter,
                                            [FromQuery] SortingFilter sortingFilter,
                                            [FromQuery] string filterBy = "")
	{
        var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
        var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);
		var realEstates = await realEstateService.GetAllAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                              validSortingFilter.SortField, validSortingFilter.Ascending, filterBy);
        var totalRecords = await realEstateService.GetAllRealEstatesCountAsync(filterBy);
        return Ok(PaginationHelper.CreatePageResponse(realEstates, validPaginationFilter, totalRecords));
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
			return BadRequest(new Response(false, "You did not create this real estate"));
        }
        
        return Ok(realEstate);
    }

    [ValidateFilters]
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
            return BadRequest(new Response(false, "You did not create this real estate"));
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
            return BadRequest(new Response(false, "You did not create this real estate"));
        }
        
		return NoContent();
	}

    [AllowAnonymous]
    [HttpGet("[action]")]
    public IActionResult GetSortFields()
    {
        return Ok(SortingHelper.GetSortFields().Select(x => x.Key));
    }
}

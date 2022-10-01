namespace WasteSegregation.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/realEstate/{realEstateId}/wasteBags")]
public class WasteBagsController : ControllerBase
{
    private readonly IWasteBagsService wasteBagsService;
    private readonly ICreatedByUserService createdByUserService;

    public WasteBagsController(IWasteBagsService wasteBagsService, ICreatedByUserService createdByUserService)
    {
        this.wasteBagsService = wasteBagsService;
        this.createdByUserService = createdByUserService;
    }

    [Authorize(Roles = UserRoles.User)]
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] int realEstateId, [FromBody] CreateWasteBagsDto createWasteBagsDto)
    {
        var isUserCreated = await createdByUserService.IsUserCreatedAsync(realEstateId,
                                                                          User.FindFirstValue(ClaimTypes.NameIdentifier));
        var newWasteBags = await wasteBagsService.AddAsync(realEstateId,
                                                           createWasteBagsDto,
                                                           User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (!isUserCreated)
        {
            return BadRequest(new Response<bool>()
            {
                Succeeded = false,
                Message = "You did not create this real estate. You can not add waste bags."
            });
        }
        
        return Created($"api/realEstate/{realEstateId}/wasteBags/{newWasteBags}", null);
    }
}

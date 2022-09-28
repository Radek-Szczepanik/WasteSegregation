namespace WasteSegregation.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/realEstate/{realEstateId}/wasteBags")]
public class WasteBagsController : ControllerBase
{
    private readonly IWasteBagsService wasteBagsService;

    public WasteBagsController(IWasteBagsService wasteBagsService)
    {
        this.wasteBagsService = wasteBagsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] int realEstateId, [FromBody] CreateWasteBagsDto createWasteBagsDto)
    {
        var userOwnsRealEstate = await wasteBagsService.RealEstateWasteBagsAsync(realEstateId,
                                                                                 User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (!userOwnsRealEstate)
        {
            return BadRequest(new Response<bool>()
            {
                Succeeded = false,
                Message = "You did not create this real estate. You can not add waste bags."
            });
        }
        
        var newWasteBags = await wasteBagsService.AddAsync(realEstateId,
                                                           createWasteBagsDto,
                                                           User.FindFirstValue(ClaimTypes.NameIdentifier));
        return Created($"api/realEstate/{realEstateId}/wasteBags/{newWasteBags}", null);
    }
}

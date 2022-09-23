namespace WasteSegregation.WebAPI.Controllers;

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
        var newWasteBags = await wasteBagsService.AddAsync(realEstateId, createWasteBagsDto);
        return Created($"api/realEstate/{realEstateId}/wasteBags/{newWasteBags}", null);
    }
}

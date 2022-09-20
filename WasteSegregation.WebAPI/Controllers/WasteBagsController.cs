namespace WasteSegregation.WebAPI.Controllers;

[ApiController]
[Route("api/realEstate/{realEstateId}/wasteBags")]
public class WasteBagsController : ControllerBase
{
    private readonly IWasteBagsService wasteService;

    public WasteBagsController(IWasteBagsService wasteService)
    {
        this.wasteService = wasteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] int realEstateId, [FromBody] CreateWasteBagsDto createWasteBagsDto)
    {
        var newWasteBags = await wasteService.AddAsync(realEstateId, createWasteBagsDto);
        return Created($"api/realEstate/{realEstateId}/wasteBags/{newWasteBags}", null);
    }
}

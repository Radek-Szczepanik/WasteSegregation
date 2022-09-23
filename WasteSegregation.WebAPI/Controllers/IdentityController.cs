namespace WasteSegregation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;

    public IdentityController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var userExist = await userManager.FindByEmailAsync(registerModel.Email);
        if (userExist != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
            {
                Succeeded = false,
                Message = "User already exists!"
            });
        }

        ApplicationUser user = new ApplicationUser()
        {
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.Username
        };

        var result = await userManager.CreateAsync(user, registerModel.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
            {
                Succeeded = false,
                Message = "User creation failed!",
                Errors = result.Errors.Select(x => x.Description)
            });
        }

        return Ok(new Response<bool> { Succeeded = true, Message = "User created successfully!" });
    }
}

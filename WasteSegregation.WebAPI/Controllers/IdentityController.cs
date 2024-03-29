﻿namespace WasteSegregation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration configuration;

    public IdentityController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
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

        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        await userManager.AddToRoleAsync(user, UserRoles.User);

        return Ok(new Response<bool> { Succeeded = true, Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("RegisterAdmin")]
    public async Task<IActionResult> RegisterAdmin(RegisterModel registerModel)
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

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        await userManager.AddToRoleAsync(user, UserRoles.Admin);

        return Ok(new Response<bool> { Succeeded = true, Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginModel login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }
}

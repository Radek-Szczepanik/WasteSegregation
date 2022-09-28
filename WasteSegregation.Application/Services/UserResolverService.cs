namespace WasteSegregation.Application.Services;

public class UserResolverService
{
	private readonly IHttpContextAccessor contextAccessor;

	public UserResolverService(IHttpContextAccessor contextAccessor)
	{
		this.contextAccessor = contextAccessor;
	}

	public string GetUser()
	{
		return contextAccessor.HttpContext.User?.Identity?.Name;
	}
}

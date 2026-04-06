using System.Security.Claims;
using CleanTeeth.Application.Contracts.Security;
using Microsoft.AspNetCore.Http;

namespace CleanTeeth.Security.Services;

public class UserService: IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
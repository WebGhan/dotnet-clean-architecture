using CleanTeeth.Application.Contracts.Security;
using CleanTeeth.Security.Models;
using CleanTeeth.Security.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Security;

public static class RegisterSecurityServices
{
    public static void AddSecurityServices(this IServiceCollection services)
    {
        services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorization(options =>
        {
            options.AddPolicy("isAdmin", policy => policy.RequireClaim("isAdmin"));
        });

        services.AddDbContext<CleanTeethSecurityDbContext>(options =>
            options.UseNpgsql("name=CleanTeethConnectionString"));

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<CleanTeethSecurityDbContext>()
            .AddApiEndpoints();

        services.AddTransient<IUserService, UserService>();
        services.AddHttpContextAccessor();
    }
}
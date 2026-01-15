using System.Security.Claims;
using MiniCatalog.Domain.Constants;

namespace MiniCatalog.Api.Configurations;

public static class AuthorizationConfiguration
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.Admin, policy => 
                policy.RequireClaim(ClaimTypes.Role, "Admin"));
            
            options.AddPolicy(Policies.Editor, policy => 
                policy.RequireClaim(ClaimTypes.Role, "Admin", "Editor"));
            
            options.AddPolicy(Policies.Viewer, policy => 
                policy.RequireClaim(ClaimTypes.Role, "Admin", "Editor", "Viewer"));
        });

        return services;
    }
}
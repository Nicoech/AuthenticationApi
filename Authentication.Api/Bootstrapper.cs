using AuthenticationApi.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi;

public static class Bootstrapper
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = config["KeyCloakCredentials:Realm"];
                options.Audience = config["KeyCloakCredentials:Audience"];
                options.RequireHttpsMetadata = false;
            });
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(b => b.MigrationsAssembly("Authentication.Infrastructure"));
            options.UseSqlServer(config["ConnectionStrings:AuthenticationApiDb"]);
        });
    }
}
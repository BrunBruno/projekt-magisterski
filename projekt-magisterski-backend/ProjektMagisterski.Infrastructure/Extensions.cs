
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjektMagisterski.Infrastructure.Contexts;
using ProjektMagisterski.Infrastructure.Options;
using ProjektMagisterski.Shared.Options;

namespace ProjektMagisterski.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddPostgres(configuration);

        services.AddCors(options => {
            options.AddPolicy("FrontEndClient", builder => {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
        });

        return services;
    }

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration) 
    {
        var options = configuration.GetOptions<PostgresOptions>("Postgres");

        services.AddDbContext<AppDbContext>(ctx
           => ctx.UseNpgsql(options.ConnectionString));

        return services;
    }
}



using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ProjektMagisterski.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    { 
        return app;
    }
}

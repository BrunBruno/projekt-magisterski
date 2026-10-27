

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ProjektMagisterski.Application;
public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}

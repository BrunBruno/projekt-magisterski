using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Application.Services;
using ProjektMagisterski.Infrastructure.Contexts;
using ProjektMagisterski.Infrastructure.Options;
using ProjektMagisterski.Infrastructure.Repositories;
using ProjektMagisterski.Infrastructure.Workers;
using ProjektMagisterski.Shared.Options;

namespace ProjektMagisterski.Infrastructure;

public static class Extensions {
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration) {
		services.AddHttpContextAccessor();

		services.AddPostgres(configuration);
		services.AddRepositories();
		services.AddSimulationEngine();

		services.AddCors(options => {
			options.AddPolicy("FrontEndClient", builder => {
				builder.WithOrigins("http://localhost:5173", "http://localhost:3000") 
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials();
			});
		});

		return services;
	}

	public static IServiceCollection AddPostgres(
		this IServiceCollection services,
		IConfiguration configuration) {
		var options = configuration.GetOptions<PostgresOptions>("Postgres");

		services.AddDbContext<AppDbContext>(ctx =>
			ctx.UseNpgsql(options.ConnectionString));

		return services;
	}

	public static IServiceCollection AddRepositories(
		this IServiceCollection services) {
		services.AddScoped<IFinancialInstrumentRepository, FinancialInstrumentRepository>();
		services.AddScoped<IMarketDataRepository, MarketDataRepository>();

		services.AddScoped<ITradingStrategyRepository, TradingStrategyRepository>();
		services.AddScoped<IStrategyParameterRepository, StrategyParameterRepository>();

		services.AddScoped<ISimulationRepository, SimulationRepository>();
		services.AddScoped<ISimulationAgentRepository, SimulationAgentRepository>();
		services.AddScoped<ISimulationConfigurationRepository, SimulationConfigurationRepository>();
		services.AddScoped<ISimulationEventRepository, SimulationEventRepository>();

		services.AddScoped<IPortfolioRepository, PortfolioRepository>();
		services.AddScoped<IPositionRepository, PositionRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<ITradeRepository, TradeRepository>();

		services.AddScoped<IAgentSnapshotRepository, AgentSnapshotRepository>();
		services.AddScoped<IAgentPerformanceRepository, AgentPerformanceRepository>();

		return services;
	}
	public static IServiceCollection AddSimulationEngine(
		this IServiceCollection services) {
		services.AddSingleton<ISimulationQueue, InMemorySimulationQueue>();
		services.AddHostedService<SimulationWorker>();

		return services;
	}

}
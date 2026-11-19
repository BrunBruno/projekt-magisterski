using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Configuration;

namespace ProjektMagisterski.Infrastructure.Contexts;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

	public DbSet<FinancialInstrument> FinancialInstruments => Set<FinancialInstrument>();
	public DbSet<MarketData> MarketData => Set<MarketData>();

	public DbSet<TradingStrategy> TradingStrategies => Set<TradingStrategy>();
	public DbSet<StrategyParameter> StrategyParameters => Set<StrategyParameter>();

	public DbSet<Simulation> Simulations => Set<Simulation>();
	public DbSet<SimulationAgent> SimulationAgents => Set<SimulationAgent>();
	public DbSet<SimulationConfiguration> SimulationConfigurations => Set<SimulationConfiguration>();
	public DbSet<SimulationEvent> SimulationEvents => Set<SimulationEvent>();

	public DbSet<Portfolio> Portfolios => Set<Portfolio>();
	public DbSet<Position> Positions => Set<Position>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<Trade> Trades => Set<Trade>();

	public DbSet<AgentSnapshot> AgentSnapshots => Set<AgentSnapshot>();
	public DbSet<AgentPerformance> AgentPerformances => Set<AgentPerformance>();

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		var configuration = new DbContextConfiguration();
		configuration.Configure(builder);
	}
}

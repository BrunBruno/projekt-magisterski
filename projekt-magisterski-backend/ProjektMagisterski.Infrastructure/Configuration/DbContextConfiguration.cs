using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Infrastructure.Configuration;

public class DbContextConfiguration {

	public void Configure(ModelBuilder builder) {
		ConfigureFinancialInstrument(builder);
		ConfigureMarketData(builder);

		ConfigureTradingStrategy(builder);
		ConfigureStrategyParameter(builder);

		ConfigureSimulation(builder);
		ConfigureSimulationAgent(builder);
		ConfigureSimulationConfiguration(builder);
		ConfigureSimulationEvent(builder);

		ConfigurePortfolio(builder);
		ConfigurePosition(builder);
		ConfigureOrder(builder);
		ConfigureTrade(builder);

		ConfigureAgentSnapshot(builder);
		ConfigureAgentPerformance(builder);
	}

	private static void ConfigureFinancialInstrument(ModelBuilder builder) {
		builder.Entity<FinancialInstrument>(entity => {
			entity.ToTable("financial_instruments");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Symbol)
				.IsRequired()
				.HasMaxLength(32);

			entity.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.Type)
				.HasConversion<string>()
				.HasMaxLength(32);

			entity.Property(x => x.Currency)
				.IsRequired()
				.HasMaxLength(10);

			entity.Property(x => x.Exchange)
				.HasMaxLength(100);

			entity.Property(x => x.Description)
				.HasMaxLength(1000);

			entity.HasIndex(x => new { x.Symbol, x.Exchange })
				.IsUnique();
		});
	}

	private static void ConfigureMarketData(ModelBuilder builder) {
		builder.Entity<MarketData>(entity => {
			entity.ToTable("market_data");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Open)
				.HasPrecision(20, 8);

			entity.Property(x => x.High)
				.HasPrecision(20, 8);

			entity.Property(x => x.Low)
				.HasPrecision(20, 8);

			entity.Property(x => x.Close)
				.HasPrecision(20, 8);

			entity.Property(x => x.Volume)
				.HasPrecision(28, 8);

			entity.Property(x => x.Timeframe)
				.HasConversion<string>()
				.HasMaxLength(20);

			entity.HasOne<FinancialInstrument>()
				.WithMany()
				.HasForeignKey(x => x.FinancialInstrumentId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasIndex(x => new {
				x.FinancialInstrumentId,
				x.Timeframe,
				x.Timestamp
			}).IsUnique();
		});
	}

	private static void ConfigureTradingStrategy(ModelBuilder builder) {
		builder.Entity<TradingStrategy>(entity => {
			entity.ToTable("trading_strategies");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Code)
				.IsRequired()
				.HasMaxLength(64);

			entity.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.Description)
				.HasMaxLength(1000);

			entity.Property(x => x.Category)
				.HasConversion<string>()
				.HasMaxLength(32);

			entity.HasIndex(x => x.Code)
				.IsUnique();
		});
	}

	private static void ConfigureStrategyParameter(ModelBuilder builder) {
		builder.Entity<StrategyParameter>(entity => {
			entity.ToTable("strategy_parameters");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			entity.Property(x => x.DisplayName)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.DataType)
				.HasConversion<string>()
				.HasMaxLength(20);

			entity.Property(x => x.DefaultValue)
				.IsRequired()
				.HasMaxLength(500);

			entity.Property(x => x.MinValue)
				.HasPrecision(20, 8);

			entity.Property(x => x.MaxValue)
				.HasPrecision(20, 8);

			entity.Property(x => x.Description)
				.HasMaxLength(500);

			entity.HasOne<TradingStrategy>()
				.WithMany()
				.HasForeignKey(x => x.TradingStrategyId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => new {
				x.TradingStrategyId,
				x.Name
			}).IsUnique();
		});
	}

	private static void ConfigureSimulation(ModelBuilder builder) {
		builder.Entity<Simulation>(entity => {
			entity.ToTable("simulations");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.InitialCapital)
				.HasPrecision(20, 8);

			entity.Property(x => x.Timeframe)
				.HasConversion<string>()
				.HasMaxLength(20);

			entity.Property(x => x.Status)
				.HasConversion<string>()
				.HasMaxLength(20);

			entity.HasOne<FinancialInstrument>()
				.WithMany()
				.HasForeignKey(x => x.FinancialInstrumentId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasIndex(x => x.Status);
			entity.HasIndex(x => x.CreatedAt);
		});
	}

	private static void ConfigureSimulationAgent(ModelBuilder builder) {
		builder.Entity<SimulationAgent>(entity => {
			entity.ToTable("simulation_agents");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.InitialCapital)
				.HasPrecision(20, 8);

			entity.Property(x => x.Status)
				.HasConversion<string>()
				.HasMaxLength(20);

			entity.Property(x => x.ConfigurationJson)
				.HasColumnType("jsonb")
				.IsRequired();

			entity.HasOne<Simulation>()
				.WithMany()
				.HasForeignKey(x => x.SimulationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne<TradingStrategy>()
				.WithMany()
				.HasForeignKey(x => x.TradingStrategyId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasIndex(x => x.SimulationId);
		});
	}

	private static void ConfigureSimulationConfiguration(ModelBuilder builder) {
		builder.Entity<SimulationConfiguration>(entity => {
			entity.ToTable("simulation_configurations");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.TransactionFeeRate)
				.HasPrecision(12, 8);

			entity.Property(x => x.SlippageBasisPoints)
				.HasPrecision(12, 4);

			entity.Property(x => x.MaxPositionSizePercent)
				.HasPrecision(8, 4);

			entity.HasOne<Simulation>()
				.WithOne()
				.HasForeignKey<SimulationConfiguration>(x => x.SimulationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => x.SimulationId)
				.IsUnique();
		});
	}

	private static void ConfigureSimulationEvent(ModelBuilder builder) {
		builder.Entity<SimulationEvent>(entity => {
			entity.ToTable("simulation_events");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.EventType)
				.HasConversion<string>()
				.HasMaxLength(32);

			entity.Property(x => x.Message)
				.IsRequired()
				.HasMaxLength(1000);

			entity.Property(x => x.MetadataJson)
				.HasColumnType("jsonb");

			entity.HasOne<Simulation>()
				.WithMany()
				.HasForeignKey(x => x.SimulationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne<SimulationAgent>()
				.WithMany()
				.HasForeignKey(x => x.SimulationAgentId)
				.OnDelete(DeleteBehavior.SetNull);

			entity.HasIndex(x => new {
				x.SimulationId,
				x.Timestamp
			});
		});
	}

	private static void ConfigurePortfolio(ModelBuilder builder) {
		builder.Entity<Portfolio>(entity => {
			entity.ToTable("portfolios");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.CashBalance)
				.HasPrecision(20, 8);

			entity.HasOne<SimulationAgent>()
				.WithOne()
				.HasForeignKey<Portfolio>(x => x.SimulationAgentId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => x.SimulationAgentId)
				.IsUnique();
		});
	}

	private static void ConfigurePosition(ModelBuilder builder) {
		builder.Entity<Position>(entity => {
			entity.ToTable("positions");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Side)
				.HasConversion<string>()
				.HasMaxLength(16);

			entity.Property(x => x.Quantity)
				.HasPrecision(20, 8);

			entity.Property(x => x.AverageEntryPrice)
				.HasPrecision(20, 8);

			entity.HasOne<Portfolio>()
				.WithMany()
				.HasForeignKey(x => x.PortfolioId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne<FinancialInstrument>()
				.WithMany()
				.HasForeignKey(x => x.FinancialInstrumentId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasIndex(x => new {
				x.PortfolioId,
				x.FinancialInstrumentId,
				x.Side
			});
		});
	}

	private static void ConfigureOrder(ModelBuilder builder) {
		builder.Entity<Order>(entity => {
			entity.ToTable("orders");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Type)
				.HasConversion<string>()
				.HasMaxLength(16);

			entity.Property(x => x.Side)
				.HasConversion<string>()
				.HasMaxLength(16);

			entity.Property(x => x.Status)
				.HasConversion<string>()
				.HasMaxLength(24);

			entity.Property(x => x.Quantity)
				.HasPrecision(20, 8);

			entity.Property(x => x.FilledQuantity)
				.HasPrecision(20, 8);

			entity.Property(x => x.LimitPrice)
				.HasPrecision(20, 8);

			entity.HasOne<SimulationAgent>()
				.WithMany()
				.HasForeignKey(x => x.SimulationAgentId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => new {
				x.SimulationAgentId,
				x.CreatedAt
			});
		});
	}

	private static void ConfigureTrade(ModelBuilder builder) {
		builder.Entity<Trade>(entity => {
			entity.ToTable("trades");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.Side)
				.HasConversion<string>()
				.HasMaxLength(16);

			entity.Property(x => x.Quantity)
				.HasPrecision(20, 8);

			entity.Property(x => x.ExecutionPrice)
				.HasPrecision(20, 8);

			entity.Property(x => x.Fee)
				.HasPrecision(20, 8);

			entity.Property(x => x.Slippage)
				.HasPrecision(20, 8);

			entity.HasOne<Order>()
				.WithMany()
				.HasForeignKey(x => x.OrderId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne<SimulationAgent>()
				.WithMany()
				.HasForeignKey(x => x.SimulationAgentId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => new {
				x.SimulationAgentId,
				x.ExecutedAt
			});
		});
	}

	private static void ConfigureAgentSnapshot(ModelBuilder builder) {
		builder.Entity<AgentSnapshot>(entity => {
			entity.ToTable("agent_snapshots");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.CashBalance)
				.HasPrecision(20, 8);

			entity.Property(x => x.PositionsValue)
				.HasPrecision(20, 8);

			entity.Property(x => x.TotalEquity)
				.HasPrecision(20, 8);

			entity.Property(x => x.RealizedProfitLoss)
				.HasPrecision(20, 8);

			entity.Property(x => x.UnrealizedProfitLoss)
				.HasPrecision(20, 8);

			entity.HasOne<SimulationAgent>()
				.WithMany()
				.HasForeignKey(x => x.SimulationAgentId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => new {
				x.SimulationAgentId,
				x.StepNumber
			}).IsUnique();
		});
	}

	private static void ConfigureAgentPerformance(ModelBuilder builder) {
		builder.Entity<AgentPerformance>(entity => {
			entity.ToTable("agent_performances");

			entity.HasKey(x => x.Id);

			entity.Property(x => x.InitialCapital)
				.HasPrecision(20, 8);

			entity.Property(x => x.FinalCapital)
				.HasPrecision(20, 8);

			entity.Property(x => x.TotalReturnPercentage)
				.HasPrecision(12, 4);

			entity.Property(x => x.MaxDrawdown)
				.HasPrecision(12, 4);

			entity.Property(x => x.WinRate)
				.HasPrecision(12, 4);

			entity.Property(x => x.AverageProfit)
				.HasPrecision(20, 8);

			entity.Property(x => x.AverageLoss)
				.HasPrecision(20, 8);

			entity.Property(x => x.ProfitFactor)
				.HasPrecision(12, 4);

			entity.Property(x => x.SharpeRatio)
				.HasPrecision(12, 6);

			entity.HasOne<SimulationAgent>()
				.WithMany()
				.HasForeignKey(x => x.SimulationAgentId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasIndex(x => x.SimulationAgentId)
				.IsUnique();
		});
	}
}

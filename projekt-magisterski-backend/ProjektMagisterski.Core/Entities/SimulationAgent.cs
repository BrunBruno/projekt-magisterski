using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class SimulationAgent : BaseEntity {
	public Guid SimulationId { get; private set; }

	public Guid TradingStrategyId { get; private set; }

	public string Name { get; private set; } = string.Empty;

	public decimal InitialCapital { get; private set; }

	public AgentStatus Status { get; private set; }

	// Konfiguracja parametrów strategii w formacie JSON.
	public string ConfigurationJson { get; private set; } = "{}";

	private SimulationAgent() { }

	public SimulationAgent(
		Guid simulationId,
		Guid tradingStrategyId,
		string name,
		decimal initialCapital,
		string configurationJson = "{}") {
		if (simulationId == Guid.Empty || tradingStrategyId == Guid.Empty)
			throw new ArgumentException("Simulation and strategy are required.");

		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.");

		if (initialCapital <= 0)
			throw new ArgumentException("Initial capital must be positive.");

		SimulationId = simulationId;
		TradingStrategyId = tradingStrategyId;
		Name = name.Trim();
		InitialCapital = initialCapital;
		ConfigurationJson = configurationJson;
		Status = AgentStatus.Ready;
	}
}

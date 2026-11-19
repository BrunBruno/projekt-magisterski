using ProjektMagisterski.Core.Abstract;

namespace ProjektMagisterski.Core.Entities;

public class AgentSnapshot : BaseEntity {
	public Guid SimulationAgentId { get; private set; }

	// Numer kroku symulacji.
	public long StepNumber { get; private set; }

	public DateTimeOffset Timestamp { get; private set; }

	public decimal CashBalance { get; private set; }

	public decimal PositionsValue { get; private set; }

	// CashBalance + PositionsValue.
	public decimal TotalEquity { get; private set; }

	public decimal RealizedProfitLoss { get; private set; }

	public decimal UnrealizedProfitLoss { get; private set; }

	private AgentSnapshot() { }

	public AgentSnapshot(
		Guid simulationAgentId,
		long stepNumber,
		DateTimeOffset timestamp,
		decimal cashBalance,
		decimal positionsValue,
		decimal realizedProfitLoss,
		decimal unrealizedProfitLoss) {
		if (simulationAgentId == Guid.Empty)
			throw new ArgumentException("Agent is required.");

		if (stepNumber < 0)
			throw new ArgumentException("Invalid step number.");

		if (cashBalance < 0 || positionsValue < 0)
			throw new ArgumentException("Invalid portfolio values.");

		SimulationAgentId = simulationAgentId;
		StepNumber = stepNumber;
		Timestamp = timestamp.ToUniversalTime();

		CashBalance = cashBalance;
		PositionsValue = positionsValue;
		TotalEquity = cashBalance + positionsValue;

		RealizedProfitLoss = realizedProfitLoss;
		UnrealizedProfitLoss = unrealizedProfitLoss;
	}
}

using ProjektMagisterski.Core.Abstract;

namespace ProjektMagisterski.Core.Entities;

public class Portfolio : BaseEntity {
	public Guid SimulationAgentId { get; private set; }

	public decimal CashBalance { get; private set; }

	public DateTimeOffset UpdatedAt { get; private set; }

	private Portfolio() { }

	public Portfolio(Guid simulationAgentId, decimal initialCapital) {
		if (simulationAgentId == Guid.Empty)
			throw new ArgumentException("Agent is required.");

		if (initialCapital < 0)
			throw new ArgumentException("Capital cannot be negative.");

		SimulationAgentId = simulationAgentId;
		CashBalance = initialCapital;
		UpdatedAt = DateTimeOffset.UtcNow;
	}
}

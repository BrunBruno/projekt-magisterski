

using ProjektMagisterski.Core.Abstract;

namespace ProjektMagisterski.Core.Entities;

public class SimulationConfiguration : BaseEntity {
	public Guid SimulationId { get; private set; }

	// Prowizja wyrażona jako ułamek, np. 0.001 = 0.1%.
	public decimal TransactionFeeRate { get; private set; }

	// Poślizg cenowy w punktach bazowych, np. 5 = 0.05%.
	public decimal SlippageBasisPoints { get; private set; }

	public bool AllowShortSelling { get; private set; }

	// Maksymalny udział wartości portfela przeznaczony na pozycję.
	public decimal MaxPositionSizePercent { get; private set; }

	private SimulationConfiguration() { }

	public SimulationConfiguration(
		Guid simulationId,
		decimal transactionFeeRate,
		decimal slippageBasisPoints,
		bool allowShortSelling,
		decimal maxPositionSizePercent) {
		if (simulationId == Guid.Empty)
			throw new ArgumentException("Simulation is required.");

		if (transactionFeeRate < 0 || transactionFeeRate >= 1)
			throw new ArgumentException("Invalid transaction fee.");

		if (slippageBasisPoints < 0)
			throw new ArgumentException("Slippage cannot be negative.");

		if (maxPositionSizePercent <= 0 || maxPositionSizePercent > 100)
			throw new ArgumentException("Invalid position size.");

		SimulationId = simulationId;
		TransactionFeeRate = transactionFeeRate;
		SlippageBasisPoints = slippageBasisPoints;
		AllowShortSelling = allowShortSelling;
		MaxPositionSizePercent = maxPositionSizePercent;
	}
}
using ProjektMagisterski.Core.Abstract;

namespace ProjektMagisterski.Core.Entities;

public class AgentPerformance : BaseEntity {
	public Guid SimulationAgentId { get; private set; }

	public decimal InitialCapital { get; private set; }

	public decimal FinalCapital { get; private set; }

	// Wartość procentowa, np. 15.5 oznacza 15.5%.
	public decimal TotalReturnPercentage { get; private set; }

	// Maksymalne obsunięcie wyrażone w procentach.
	public decimal MaxDrawdown { get; private set; }

	public int NumberOfTrades { get; private set; }

	public int WinningTrades { get; private set; }

	public int LosingTrades { get; private set; }

	public decimal WinRate { get; private set; }

	public decimal AverageProfit { get; private set; }

	public decimal AverageLoss { get; private set; }

	public decimal ProfitFactor { get; private set; }

	public decimal? SharpeRatio { get; private set; }

	public DateTimeOffset CalculatedAt { get; private set; }

	private AgentPerformance() { }

	public AgentPerformance(
		Guid simulationAgentId,
		decimal initialCapital,
		decimal finalCapital,
		decimal totalReturnPercentage,
		decimal maxDrawdown,
		int numberOfTrades,
		int winningTrades,
		int losingTrades,
		decimal averageProfit,
		decimal averageLoss,
		decimal profitFactor,
		decimal? sharpeRatio) {
		if (simulationAgentId == Guid.Empty)
			throw new ArgumentException("Agent is required.");

		if (initialCapital <= 0 || finalCapital < 0)
			throw new ArgumentException("Invalid capital values.");

		if (numberOfTrades < 0 ||
			winningTrades < 0 ||
			losingTrades < 0 ||
			winningTrades + losingTrades > numberOfTrades)
			throw new ArgumentException("Invalid trade statistics.");

		if (maxDrawdown < 0 || maxDrawdown > 100)
			throw new ArgumentException("Invalid maximum drawdown.");

		SimulationAgentId = simulationAgentId;
		InitialCapital = initialCapital;
		FinalCapital = finalCapital;
		TotalReturnPercentage = totalReturnPercentage;
		MaxDrawdown = maxDrawdown;

		NumberOfTrades = numberOfTrades;
		WinningTrades = winningTrades;
		LosingTrades = losingTrades;

		WinRate = numberOfTrades == 0
			? 0
			: (decimal)winningTrades / numberOfTrades * 100;

		AverageProfit = averageProfit;
		AverageLoss = averageLoss;
		ProfitFactor = profitFactor;
		SharpeRatio = sharpeRatio;

		CalculatedAt = DateTimeOffset.UtcNow;
	}
}
using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class Trade : BaseEntity {
	public Guid OrderId { get; private set; }

	public Guid SimulationAgentId { get; private set; }

	public OrderSide Side { get; private set; }

	public decimal Quantity { get; private set; }

	public decimal ExecutionPrice { get; private set; }

	public decimal Fee { get; private set; }

	public decimal Slippage { get; private set; }

	public DateTimeOffset ExecutedAt { get; private set; }

	private Trade() { }

	public Trade(
		Guid orderId,
		Guid simulationAgentId,
		OrderSide side,
		decimal quantity,
		decimal executionPrice,
		decimal fee,
		decimal slippage,
		DateTimeOffset executedAt) {
		if (orderId == Guid.Empty || simulationAgentId == Guid.Empty)
			throw new ArgumentException("Order and agent are required.");

		if (quantity <= 0 || executionPrice <= 0)
			throw new ArgumentException("Invalid trade values.");

		if (fee < 0 || slippage < 0)
			throw new ArgumentException("Costs cannot be negative.");

		OrderId = orderId;
		SimulationAgentId = simulationAgentId;
		Side = side;
		Quantity = quantity;
		ExecutionPrice = executionPrice;
		Fee = fee;
		Slippage = slippage;
		ExecutedAt = executedAt.ToUniversalTime();
	}
}

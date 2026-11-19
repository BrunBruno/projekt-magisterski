
using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class Order : BaseEntity {
	public Guid SimulationAgentId { get; private set; }

	public OrderType Type { get; private set; }

	public OrderSide Side { get; private set; }

	public decimal Quantity { get; private set; }

	public decimal? LimitPrice { get; private set; }

	public decimal FilledQuantity { get; private set; }

	public OrderStatus Status { get; private set; }

	public DateTimeOffset CreatedAt { get; private set; }

	public DateTimeOffset? ExecutedAt { get; private set; }

	private Order() { }

	public Order(
		Guid simulationAgentId,
		OrderType type,
		OrderSide side,
		decimal quantity,
		decimal? limitPrice = null) {
		if (simulationAgentId == Guid.Empty)
			throw new ArgumentException("Agent is required.");

		if (quantity <= 0)
			throw new ArgumentException("Quantity must be positive.");

		if (type == OrderType.Limit && (!limitPrice.HasValue || limitPrice <= 0))
			throw new ArgumentException("Limit orders require a valid price.");

		SimulationAgentId = simulationAgentId;
		Type = type;
		Side = side;
		Quantity = quantity;
		LimitPrice = limitPrice;
		FilledQuantity = 0;
		Status = OrderStatus.Pending;
		CreatedAt = DateTimeOffset.UtcNow;
	}
}

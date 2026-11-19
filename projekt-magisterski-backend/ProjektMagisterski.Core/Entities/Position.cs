using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class Position : BaseEntity {
	public Guid PortfolioId { get; private set; }

	public Guid FinancialInstrumentId { get; private set; }

	public PositionSide Side { get; private set; }

	public decimal Quantity { get; private set; }

	public decimal AverageEntryPrice { get; private set; }

	public DateTimeOffset OpenedAt { get; private set; }

	private Position() { }

	public Position(
		Guid portfolioId,
		Guid financialInstrumentId,
		PositionSide side,
		decimal quantity,
		decimal averageEntryPrice,
		DateTimeOffset openedAt) {
		if (portfolioId == Guid.Empty || financialInstrumentId == Guid.Empty)
			throw new ArgumentException("Portfolio and instrument are required.");

		if (quantity <= 0 || averageEntryPrice <= 0)
			throw new ArgumentException("Invalid position values.");

		PortfolioId = portfolioId;
		FinancialInstrumentId = financialInstrumentId;
		Side = side;
		Quantity = quantity;
		AverageEntryPrice = averageEntryPrice;
		OpenedAt = openedAt.ToUniversalTime();
	}
}

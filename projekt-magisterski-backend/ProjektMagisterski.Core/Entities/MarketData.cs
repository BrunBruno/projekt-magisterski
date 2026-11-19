using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class MarketData : BaseEntity {
	public Guid FinancialInstrumentId { get; private set; }
	public DateTimeOffset Timestamp { get; private set; }
	public decimal Open { get; private set; }
	public decimal High { get; private set; }
	public decimal Low { get; private set; }
	public decimal Close { get; private set; }
	public decimal Volume { get; private set; }
	public Timeframe Timeframe { get; private set; }

	private MarketData() { }

	public MarketData(
		Guid financialInstrumentId,
		DateTimeOffset timestamp,
		decimal open,
		decimal high,
		decimal low,
		decimal close,
		decimal volume,
		Timeframe timeframe) 
	{
		if (financialInstrumentId == Guid.Empty)
			throw new ArgumentException("Financial instrument is required.");

		if (open <= 0 || high <= 0 || low <= 0 || close <= 0)
			throw new ArgumentException("Prices must be greater than zero.");

		if (high < Math.Max(open, close) ||
			low > Math.Min(open, close) ||
			high < low)
			throw new ArgumentException("Invalid OHLC values.");

		if (volume < 0)
			throw new ArgumentException("Volume cannot be negative.");

		FinancialInstrumentId = financialInstrumentId;
		Timestamp = timestamp.ToUniversalTime();

		Open = open;
		High = high;
		Low = low;
		Close = close;
		Volume = volume;

		Timeframe = timeframe;
	}
}


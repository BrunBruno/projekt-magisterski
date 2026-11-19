using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class FinancialInstrument : BaseEntity {
	public string Symbol { get; private set; } = string.Empty;
	public string Name { get; private set; } = string.Empty;
	public InstrumentType Type { get; private set; }
	public string Currency { get; private set; } = string.Empty;
	public string? Exchange { get; private set; }
	public string? Description { get; private set; }

	private FinancialInstrument() { }
	public FinancialInstrument(
		string symbol,
		string name,
		InstrumentType type,
		string currency,
		string? exchange = null,
		string? description = null)
	{
		if (string.IsNullOrWhiteSpace(symbol))
			throw new ArgumentException("Symbol is required.");

		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.");

		if (string.IsNullOrWhiteSpace(currency))
			throw new ArgumentException("Currency is required.");

		Symbol = symbol.Trim().ToUpperInvariant();
		Name = name.Trim();
		Type = type;
		Currency = currency.Trim().ToUpperInvariant();
		Exchange = exchange?.Trim();
		Description = description?.Trim();
	}
}

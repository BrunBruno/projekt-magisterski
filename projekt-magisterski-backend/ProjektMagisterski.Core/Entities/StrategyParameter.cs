using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class StrategyParameter : BaseEntity {
	public Guid TradingStrategyId { get; private set; }

	public string Name { get; private set; } = string.Empty;
	public string DisplayName { get; private set; } = string.Empty;

	public ParameterDataType DataType { get; private set; }

	public string DefaultValue { get; private set; } = string.Empty;

	public decimal? MinValue { get; private set; }
	public decimal? MaxValue { get; private set; }

	public string? Description { get; private set; }

	private StrategyParameter() { }

	public StrategyParameter(
		Guid tradingStrategyId,
		string name,
		string displayName,
		ParameterDataType dataType,
		string defaultValue,
		decimal? minValue = null,
		decimal? maxValue = null,
		string? description = null) {
		if (tradingStrategyId == Guid.Empty)
			throw new ArgumentException("Strategy is required.");

		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.");

		if (minValue.HasValue && maxValue.HasValue &&
			minValue > maxValue)
			throw new ArgumentException("Invalid parameter range.");

		TradingStrategyId = tradingStrategyId;
		Name = name.Trim();
		DisplayName = displayName.Trim();
		DataType = dataType;
		DefaultValue = defaultValue;
		MinValue = minValue;
		MaxValue = maxValue;
		Description = description?.Trim();
	}
}
